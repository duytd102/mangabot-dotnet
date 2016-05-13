using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class LHMangaScript
    {
        private const string LIST_PAGE_URL = "http://lhmanga.com/danh-sach-truyen/?trang=";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(LIST_PAGE_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode ul = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("pagination"));
            HtmlNode a = ul.LastChild.Descendants().First();
            string href = a.GetAttributeValue("href", "");
            int idx = 1;
            return int.TryParse(href.Replace(LIST_PAGE_URL, ""), out idx) ? idx : 1;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(LIST_PAGE_URL + pageIndex);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode panelManga = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("panel-manga"));
            List<HtmlNode> mangaNodes = panelManga.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("manga")).ToList();
            foreach (HtmlNode mn in mangaNodes)
            {
                try
                {
                    HtmlNode nameNode = mn.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("manga-name"));
                    HtmlNode a = nameNode.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
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
                catch { }
            }

            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();

            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode chapterTable = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("table-chapter"));
            List<HtmlNode> trList = chapterTable.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach(HtmlNode tr in trList)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                if (td != null)
                {
                    HtmlNode a = td.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                    string name = a.InnerText.Trim();
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

            return chapterList;
        }

        public List<Dictionary<string, string>> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> pageList = new List<Dictionary<string, string>>();
            int index = 1;

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode chapterContent = doc.GetElementbyId("content_chap");
            List<HtmlNode> imgList = chapterContent.Descendants().Where(x => x.Name.Equals("img")).ToList();
            foreach (HtmlNode img in imgList)
            {
                string url = img.GetAttributeValue("src", "");
                if (string.IsNullOrWhiteSpace(url) == false)
                {
                    pageList.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", "Trang " + StringUtils.GenerateOrdinal(imgList.Count, index) },
                            { "url", url }
                        });

                    index++;
                }
            }

            return pageList;
        }
    }
}
