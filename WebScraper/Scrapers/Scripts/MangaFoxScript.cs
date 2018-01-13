using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class MangaFoxScript
    {
        private const string ROOT_URL = "http://mangafox.la/directory/";

        public int GetTotalPages()
        {
            string navPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*nav\\s*['|\"][^>]*?>.*?</ul>";
            string liPattern = "<li>(?<TEXT>.*?)</li>";
            string aPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<PAGINATION_URL>[^>]*?)[\"|'][^>]*?>(?<PAGINATION_NUMBER>.*?)</a>";
            try
            {
                string firstPageSrc = HttpUtils.DoGetWithDecompression(ROOT_URL);
                Match navMatch = Regex.Match(firstPageSrc, navPattern);
                MatchCollection liMatchCollection = Regex.Matches(navMatch.Value, liPattern);
                Match maxPageMatch = liMatchCollection[liMatchCollection.Count - 2];
                Match aMatch = Regex.Match(maxPageMatch.Groups["TEXT"].Value, aPattern);
                string maxPage = aMatch.Groups["PAGINATION_NUMBER"].Value;
                return int.Parse(maxPage);
            }
            catch { }
            return 0;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string mangaListPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*mangalist\\s*['|\"][^>]*?>.*?</ul>";
            string liPattern = "<li>(?<TEXT>.*?)</li>";
            string mangaTitlePattern = "<a[^>]*?class\\s*=\\s*[\"|']title[^>]*?href\\s*=\\s*[\"|'](?<MANGA_URL>.*?)[\"|'].*?>(?<MANGA_TITLE>.*?)</a>";
            string mangaPageUrl = string.Format("{0}{1}.htm", ROOT_URL, pageIndex);
            try
            {
                string mangaSrc = HttpUtils.DoGetWithDecompression(mangaPageUrl);
                Match mangaListMatch = Regex.Match(mangaSrc, mangaListPattern);
                MatchCollection liMatchCollection = Regex.Matches(mangaListMatch.Value, liPattern);
                foreach (Match li in liMatchCollection)
                {
                    Match mangaTitleMatch = Regex.Match(li.Groups["TEXT"].Value, mangaTitlePattern);
                    string name = mangaTitleMatch.Groups["MANGA_TITLE"].Value.Trim();
                    string url = mangaTitleMatch.Groups["MANGA_URL"].Value.Trim();
                    
                    if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                    {
                        mangaList.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", name },
                            { "url", url }
                        });
                    }
                }
            }
            catch { }
            
            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();
            
            string chaptersBlockPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*chapters\\s*['|\"].*?>.*?<div[^>]*?id\\s*=\\s*['|\"]\\s*footer\\s*['|\"].*?>";
            string chapterListPattern = "<ul[^>]*?class\\s*=\\s*['|\"]\\s*chlist\\s*['|\"].*?>(?<CHAPTER_LIST>.*?)</ul>";
            string chapterInfoPattern = "<li>(?<CHAPTER_INFO>.*?)</li>";
            string chapterUrlPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<CHAPTER_URL>[^>]*?)[\"|'][^>]*?class\\s*=\\s*[\"|']tips[^>]*?>(?<CHAPTER_TITLE>.*?)</a>";
            string chapterNamePattern = "<span[^>]*?class\\s*=\\s*[\"|']\\s*title.*?>(?<CHAPTER_NAME>.*?)</span>";

            try
            {
                string mangaSrc = HttpUtils.DoGetWithDecompression(mangaUrl);
                Match chaptersBlockMatch = Regex.Match(mangaSrc, chaptersBlockPattern);
                MatchCollection chapterListMatchCollection = Regex.Matches(chaptersBlockMatch.Value, chapterListPattern);
                foreach (Match chapterListMatch in chapterListMatchCollection)
                {
                    MatchCollection chapterInfoMatchCollection = Regex.Matches(chapterListMatch.Groups["CHAPTER_LIST"].Value, chapterInfoPattern);
                    foreach (Match chapterInfoMatch in chapterInfoMatchCollection)
                    {
                        Match chapterUrlMatch = Regex.Match(chapterInfoMatch.Groups["CHAPTER_INFO"].Value, chapterUrlPattern);
                        Match chapterNameMatch = Regex.Match(chapterInfoMatch.Groups["CHAPTER_INFO"].Value, chapterNamePattern);

                        string name = string.Format("{0} {1}", chapterUrlMatch.Groups["CHAPTER_TITLE"].Value, chapterNameMatch.Groups["CHAPTER_NAME"].Value);
                        string url = chapterUrlMatch.Groups["CHAPTER_URL"].Value.Trim();
                        
                        if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                        {
                            chapterList.Add(new Dictionary<string, string>()
                                {
                                    { "id", Guid.NewGuid().ToString() },
                                    { "name", name },
                                    { "url", url }
                                });
                        }
                    }
                }
            }
            catch { }

            return chapterList;
        }

        public List<Dictionary<string, string>> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> pageList = new List<Dictionary<string, string>>();
            
            string formPattern = "<form[^>]*?id\\s*=\\s*['|\"]\\s*top_bar\\s*['|\"].*?>.*?</form>";
            string optionPattern = "<option[^>]*?>(?<PAGE_INDEX>.*?)</option>";
            int index = 1;
            try
            {
                string partialChapterUrl = GetParentPath(chapterUrl);
                string chapterSrc = HttpUtils.DoGetWithDecompression(chapterUrl);
                Match formMatch = Regex.Match(chapterSrc, formPattern);
                MatchCollection optionMatchCollection = Regex.Matches(formMatch.Value, optionPattern);
                int maxPage = int.Parse(optionMatchCollection[optionMatchCollection.Count - 2].Groups["PAGE_INDEX"].Value);
                for (int i = 1; i <= maxPage; i++)
                {
                    string url = ExtractPageImage(WebUtility.HtmlDecode(string.Format("{0}{1}.html", partialChapterUrl, i)));

                    if (string.IsNullOrWhiteSpace(url) == false)
                    {
                        pageList.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", "Trang " + StringUtils.GenerateOrdinal(maxPage, index) },
                                { "url", url }
                            });

                        index++;
                    }
                }
            }
            catch { }

            return pageList;
        }

        private string ExtractPageImage(string pageUrl)
        {
            string pageBlockPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*viewer\\s*['|\"].*?>.*?</div>";
            string pagePattern = "<img[^>]*?src\\s*=\\s*['|\"]\\s*(?<PAGE_URL>.*?)\\s*['|\"].*?>";
            try
            {
                string pageSrc = HttpUtils.DoGetWithDecompression(pageUrl);
                Match pageBlockMatch = Regex.Match(pageSrc, pageBlockPattern);
                Match pageMatch = Regex.Match(pageBlockMatch.Value, pagePattern);
                string imageUrl = pageMatch.Groups["PAGE_URL"].Value;
                return WebUtility.HtmlDecode(imageUrl);
            }
            catch { }
            return "";
        }

        private string GetParentPath(string chapterUrl)
        {
            if (chapterUrl.LastIndexOf("/") > 0)
                return chapterUrl.Substring(0, chapterUrl.LastIndexOf("/") + 1);
            return chapterUrl;
        }
    }
}
