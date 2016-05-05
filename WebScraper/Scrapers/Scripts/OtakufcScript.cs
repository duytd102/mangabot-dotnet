using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class OtakufcScript
    {
        private const string BASE_LIST_URL = "http://otakufc.com/danhsach/key/all/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);
            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("button-bar"));
            if (paginator != null)
            {
                HtmlNode lastPage = paginator.Descendants().Last(x => x.Name.Equals("li"));
                return int.Parse(lastPage.FirstChild.InnerText);
            }
            return 1;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string listUrl = String.Format("{0}{1}", BASE_LIST_URL, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> blockContent = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("block_content")).ToList();
            foreach (HtmlNode bc in blockContent)
            {
                List<HtmlNode> trList = bc.Descendants().Where(x => x.Name.Equals("tr")).ToList();
                foreach (HtmlNode tr in trList)
                {
                    HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                    if (td != null)
                    {
                        HtmlNode a = td.FirstChild;
                        string name = a.InnerText.Trim();
                        string url = a.GetAttributeValue("href", "").Trim();

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
            }

            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            int slashIndex = mangaUrl.LastIndexOf("/");
            if (slashIndex == mangaUrl.Length - 1) slashIndex = mangaUrl.Substring(0, slashIndex).LastIndexOf("/");
            String lastPath = mangaUrl.Substring(slashIndex + 1);

            List<HtmlNode> blockContent = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("block_content")).ToList();
            foreach (HtmlNode bc in blockContent)
            {
                List<HtmlNode> trList = bc.Descendants().Where(x => x.Name.Equals("tr")).ToList();
                foreach (HtmlNode tr in trList)
                {
                    HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                    if (td != null)
                    {
                        HtmlNode a = td.FirstChild;
                        string name = a.InnerText.Trim();
                        string url = mangaUrl + a.GetAttributeValue("href", "").Trim().Replace(lastPath, "");
                        
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

            return chapterList;
        }

        public List<Dictionary<string, string>> GetPageList(string chapterUrl)
        {
            int index = 1;
            List<Dictionary<string, string>> pageList = new List<Dictionary<string, string>>();
            
            const string pattern = "lstImages.push\\([\"|'](?<URL>.+?)[\"|']\\)";

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            MatchCollection list = Regex.Matches(src, pattern, RegexOptions.IgnoreCase);
            foreach (Match img in list)
            {
                string url = img.Groups["URL"].Value.Trim();

                if (string.IsNullOrWhiteSpace(url) == false)
                {
                    pageList.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", "Trang " + StringUtils.GenerateOrdinal(list.Count, index) },
                            { "url", url }
                        });

                    index++;
                }
            }

            return pageList;
        }
    }
}
