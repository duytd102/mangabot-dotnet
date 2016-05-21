using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class MangaParkScript
    {
        const string DOMAIN = "http://mangapark.me/";
        const string BASE_LIST_URL = "http://mangapark.me/latest/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);
            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("paging-bar"));
            if (paginator != null)
            {
                int max = 1;
                List<HtmlNode> aEleList = paginator.Descendants().Where(x => x.Name.Equals("a")).ToList();
                foreach (HtmlNode aEle in aEleList)
                {
                    string href = aEle.GetAttributeValue("href", "");
                    int idx = 0;
                    if (int.TryParse(Regex.Replace(href, ".+/", ""), out idx))
                    {
                        if (max < idx)
                        {
                            max = idx;
                        }
                    }
                }

                return max;
            }
            return 1;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + pageIndex);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> items = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("item")).ToList();
            foreach (HtmlNode item in items)
            {
                HtmlNode a = item.Descendants().FirstOrDefault(x => x.Name.Equals("ul")).Element("h3").Element("a");
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

            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> streams = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("stream")).ToList();
            foreach (HtmlNode stream in streams)
            {
                HtmlNode h3 = stream.Element("h3");
                string version = "";
                if (h3 != null)
                {
                    HtmlNode versionNode = h3.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("st"));
                    if (versionNode != null)
                    {
                        version = versionNode.InnerText.Trim() + " - ";
                    }
                }

                List<HtmlNode> volumes = stream.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("volume")).ToList();
                foreach(HtmlNode volume in volumes)
                {
                    List<HtmlNode> chapters = volume.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("chapter")).Elements("li").ToList();
                    foreach (HtmlNode chapterNode in chapters)
                    {
                        HtmlNode chapterName = chapterNode.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                        HtmlNode a = chapterNode.Descendants().LastOrDefault(x => x.Name.Equals("a"));

                        string name = version + chapterName.InnerText.Trim();
                        string url = a.GetAttributeValue("href", "").Trim();

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

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> canvasList = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("canvas")).ToList();
            foreach (HtmlNode canvas in canvasList)
            {
                HtmlNode img = canvas.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("img-link")).Element("img");
                string url = img.GetAttributeValue("src", "").Trim();

                if (string.IsNullOrWhiteSpace(url) == false)
                {
                    pageList.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", "Trang " + StringUtils.GenerateOrdinal(canvasList.Count, index) },
                            { "url", url }
                        });

                    index++;
                }
            }

            return pageList;
        }
    }
}
