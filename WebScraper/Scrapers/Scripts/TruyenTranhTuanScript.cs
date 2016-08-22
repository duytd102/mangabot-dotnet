using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class TruyenTranhTuanScript
    {
        private const string LIST_URL = "http://truyentranhtuan.com/danh-sach-truyen";

        public int GetTotalPages()
        {
            return 1;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(LIST_URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode container = doc.GetElementbyId("new-chapter");
            List<HtmlNode> list = container.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("manga-focus")).ToList();

            foreach (HtmlNode m in list)
            {
                HtmlNode a = m.Descendants().First(x => x.GetAttributeValue("class", "").Contains("manga")).FirstChild;
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

            HtmlNode container = doc.GetElementbyId("manga-chapter");
            List<HtmlNode> list = container.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("chapter-name")).ToList();

            foreach (HtmlNode c in list)
            {
                HtmlNode a = c.FirstChild;
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

            return chapterList;
        }

        public List<Dictionary<string, string>> GetPageList(string chapterUrl)
        {
            int index = 1;
            List<Dictionary<string, string>> pageList = new List<Dictionary<string, string>>();
            const string googleServer = @"slides_page_url_path\s*=\s*\[\s*(?<urls>[^\]]+)\]";
            const string newServer = @"slides_page_path\s*=\s*\[\s*(?<urls>[^\]]+)\]";

            string src = HttpUtils.MakeHttpGet(chapterUrl);

            Match arr = Regex.Match(src, googleServer);
            if (!arr.Success)
            {
                arr = Regex.Match(src, newServer);
            }

            string grp = arr.Groups["urls"].Value;
            string[] list = grp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string u in list)
            {
                string url = u.Replace("\"", "").Replace("'", "").Trim();

                if (string.IsNullOrWhiteSpace(url) == false)
                {
                    pageList.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", "Trang " + StringUtils.GenerateOrdinal(list.Length, index) },
                            { "url", url }
                        });

                    index++;
                }
            }

            return pageList;
        }
    }
}
