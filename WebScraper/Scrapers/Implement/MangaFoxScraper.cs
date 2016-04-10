using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Utils;

namespace WebScraper.Scrapers.Implement
{
    internal class MangaFoxScraper : IScraper
    {
        const string ROOT_URL = "http://mangafox.me/directory/";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/mangafox.txt";
        const string CLASS_NAME = "WebScraper.Scrapers.Implement.MangaFoxScraper";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
            }
            else
            {
                String navPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*nav\\s*['|\"][^>]*?>.*?</ul>";
                String liPattern = "<li>(?<TEXT>.*?)</li>";
                String aPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<PAGINATION_URL>[^>]*?)[\"|'][^>]*?>(?<PAGINATION_NUMBER>.*?)</a>";
                try
                {
                    String firstPageSrc = HttpUtils.DoGetWithDecompression(ROOT_URL);
                    Match navMatch = Regex.Match(firstPageSrc, navPattern);
                    MatchCollection liMatchCollection = Regex.Matches(navMatch.Value, liPattern);
                    Match maxPageMatch = liMatchCollection[liMatchCollection.Count - 2];
                    Match aMatch = Regex.Match(maxPageMatch.Groups["TEXT"].Value, aPattern);
                    String maxPage = aMatch.Groups["PAGINATION_NUMBER"].Value;
                    return int.Parse(maxPage);
                }
                catch { }
                return 0;
            }
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<List<Manga>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
            }
            else
            {
                String mangaListPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*mangalist\\s*['|\"][^>]*?>.*?</ul>";
                String liPattern = "<li>(?<TEXT>.*?)</li>";
                String mangaTitlePattern = "<a[^>]*?class\\s*=\\s*[\"|']title[^>]*?href\\s*=\\s*[\"|'](?<MANGA_URL>.*?)[\"|'].*?>(?<MANGA_TITLE>.*?)</a>";
                String mangaPageUrl = String.Format("{0}{1}.htm", ROOT_URL, pageIndex);
                List<Manga> mangaList = new List<Manga>();
                Manga manga;
                String name, url;
                try
                {
                    String mangaSrc = HttpUtils.DoGetWithDecompression(mangaPageUrl);
                    Match mangaListMatch = Regex.Match(mangaSrc, mangaListPattern);
                    MatchCollection liMatchCollection = Regex.Matches(mangaListMatch.Value, liPattern);
                    foreach (Match li in liMatchCollection)
                    {
                        Match mangaTitleMatch = Regex.Match(li.Groups["TEXT"].Value, mangaTitlePattern);
                        name = WebUtility.HtmlDecode(mangaTitleMatch.Groups["MANGA_TITLE"].Value);
                        url = WebUtility.HtmlDecode(mangaTitleMatch.Groups["MANGA_URL"].Value);

                        manga = new Manga();
                        manga.ID = Guid.NewGuid().ToString();
                        manga.Name = name.Trim();
                        manga.Url = url.Trim();
                        manga.LocalPath = "";
                        manga.Site = MangaSite.MANGAFOX;
                        mangaList.Add(manga);
                    }
                }
                catch { }
                return mangaList;
            }
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<List<Chapter>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
            }
            else
            {
                String chaptersBlockPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*chapters\\s*['|\"].*?>.*?<div[^>]*?id\\s*=\\s*['|\"]\\s*footer\\s*['|\"].*?>";
                String chapterListPattern = "<ul[^>]*?class\\s*=\\s*['|\"]\\s*chlist\\s*['|\"].*?>(?<CHAPTER_LIST>.*?)</ul>";
                String chapterInfoPattern = "<li>(?<CHAPTER_INFO>.*?)</li>";
                String chapterUrlPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<CHAPTER_URL>[^>]*?)[\"|'][^>]*?class\\s*=\\s*[\"|']tips[^>]*?>(?<CHAPTER_TITLE>.*?)</a>";
                String chapterNamePattern = "<span[^>]*?class\\s*=\\s*[\"|']\\s*title.*?>(?<CHAPTER_NAME>.*?)</span>";
                List<Chapter> chapterList = new List<Chapter>();
                Chapter chapter;
                try
                {
                    String mangaSrc = HttpUtils.DoGetWithDecompression(mangaUrl);
                    Match chaptersBlockMatch = Regex.Match(mangaSrc, chaptersBlockPattern);
                    MatchCollection chapterListMatchCollection = Regex.Matches(chaptersBlockMatch.Value, chapterListPattern);
                    foreach (Match chapterListMatch in chapterListMatchCollection)
                    {
                        MatchCollection chapterInfoMatchCollection = Regex.Matches(chapterListMatch.Groups["CHAPTER_LIST"].Value, chapterInfoPattern);
                        foreach (Match chapterInfoMatch in chapterInfoMatchCollection)
                        {
                            Match chapterUrlMatch = Regex.Match(chapterInfoMatch.Groups["CHAPTER_INFO"].Value, chapterUrlPattern);
                            Match chapterNameMatch = Regex.Match(chapterInfoMatch.Groups["CHAPTER_INFO"].Value, chapterNamePattern);

                            String name = String.Format("{0} {1}", chapterUrlMatch.Groups["CHAPTER_TITLE"].Value, chapterNameMatch.Groups["CHAPTER_NAME"].Value);
                            String url = chapterUrlMatch.Groups["CHAPTER_URL"].Value;

                            chapter = new Chapter();
                            chapter.ID = Guid.NewGuid().ToString();
                            chapter.Name = WebUtility.HtmlDecode(name).Trim();
                            chapter.Url = WebUtility.HtmlDecode(url).Trim();
                            chapter.Site = MangaSite.MANGAFOX;
                            chapter.LocalPath = "";
                            chapterList.Add(chapter);
                        }
                    }
                }
                catch { }
                return chapterList;
            }
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<List<Page>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
            }
            else
            {
                String formPattern = "<form[^>]*?id\\s*=\\s*['|\"]\\s*top_bar\\s*['|\"].*?>.*?</form>";
                String optionPattern = "<option[^>]*?>(?<PAGE_INDEX>.*?)</option>";
                List<Page> pageList = new List<Page>();
                Page page;
                String pageUrl;
                int index = 1;
                try
                {
                    String partialChapterUrl = GetParentPath(chapterUrl);
                    String chapterSrc = HttpUtils.DoGetWithDecompression(chapterUrl);
                    Match formMatch = Regex.Match(chapterSrc, formPattern);
                    MatchCollection optionMatchCollection = Regex.Matches(formMatch.Value, optionPattern);
                    int maxPage = int.Parse(optionMatchCollection[optionMatchCollection.Count - 2].Groups["PAGE_INDEX"].Value);
                    for (int i = 1; i <= maxPage; i++)
                    {
                        pageUrl = String.Format("{0}{1}.html", partialChapterUrl, i);

                        page = new Page();
                        page.ID = Guid.NewGuid().ToString();
                        page.Name = String.Format("Trang {0}", StringUtils.GenerateOrdinal(maxPage, index));
                        page.Url = ExtractPageImage(WebUtility.HtmlDecode(pageUrl));
                        page.LocalPath = "";
                        page.Site = MangaSite.MANGAFOX;
                        pageList.Add(page);
                        index++;
                    }
                }
                catch { }
                return pageList;
            }
        }

        private String ExtractPageImage(String pageUrl)
        {
            String pageBlockPattern = "<div[^>]*?id\\s*=\\s*['|\"]\\s*viewer\\s*['|\"].*?>.*?</div>";
            String pagePattern = "<img[^>]*?src\\s*=\\s*['|\"]\\s*(?<PAGE_URL>.*?)\\s*['|\"].*?>";
            try
            {
                String pageSrc = HttpUtils.DoGetWithDecompression(pageUrl);
                Match pageBlockMatch = Regex.Match(pageSrc, pageBlockPattern);
                Match pageMatch = Regex.Match(pageBlockMatch.Value, pagePattern);
                String imageUrl = pageMatch.Groups["PAGE_URL"].Value;
                return WebUtility.HtmlDecode(imageUrl);
            }
            catch {  }
            return "";
        }

        private String GetParentPath(String chapterUrl)
        {
            if (chapterUrl.LastIndexOf("/") > 0)
                return chapterUrl.Substring(0, chapterUrl.LastIndexOf("/") + 1);
            return chapterUrl;
        }
    }
}
