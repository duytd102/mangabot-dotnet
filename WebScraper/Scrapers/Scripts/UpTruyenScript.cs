using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class UpTruyenScript
    {
        const string BASE_LIST_URL = "http://uptruyen.com/manga/tat-ca-the-loai";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode d = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("m_pagination"));
            HtmlNode last = doc.DocumentNode.Descendants()
                .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("m_pagination")).Descendants()
                .LastOrDefault(x => x.Name.Equals("li")).Element("a");
            if (last == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(Regex.Match(last.GetAttributeValue("href", ""), @".+page=(?<INDEX>\d+).+").Groups["INDEX"].Value);
            }
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();

            string listUrl = pageIndex == 1 ? BASE_LIST_URL : (BASE_LIST_URL + "?&page=" + pageIndex + "&order_by=&order_type=");
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> list = doc.DocumentNode.Descendants()
                .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("wrapper-top-view-shounen")).Descendants()
                .Where(x => x.GetAttributeValue("class", "").Contains("wrapper-top-view-common")).ToList();
            foreach (HtmlNode item in list)
            {
                HtmlNode a = item.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("top-view-content")).Descendants()
                    .FirstOrDefault(x => x.Name.Equals("a"));
                string name = a.InnerText.Trim();
                string url = a.GetAttributeValue("href", "");

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

            List<HtmlNode> list = doc.GetElementbyId("chapter_table").Descendants()
                .Where(x => x.GetAttributeValue("class", "").Contains("detail-chap-name")).ToList();
            foreach (HtmlNode item in list)
            {
                HtmlNode a = item.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                string name = a.InnerText.Trim();
                string url = a.GetAttributeValue("href", "");

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

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> imgList = doc.GetElementbyId("reader-box").Descendants().Where(x => x.Name.Equals("img")).ToList();
            foreach (HtmlNode img in imgList)
            {
                string url = img.GetAttributeValue("src", "").Trim();
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
