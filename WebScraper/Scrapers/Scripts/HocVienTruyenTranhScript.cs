using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class HocVienTruyenTranhScript
    {
        const string BASE_LIST_URL = "http://truyen.academyvn.com/manga/all?page=";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);
            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("pagination"));
            if (paginator != null)
            {
                HtmlNode a = paginator.Descendants().LastOrDefault(x => x.Name.Equals("a") && x.GetAttributeValue("rel", "").Length == 0);
                return int.Parse(a.InnerText);
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

            HtmlNode main = doc.GetElementbyId("main");
            List<HtmlNode> trList = main.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trList)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                if (td != null)
                {
                    HtmlNode a = td.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                    string name = a.FirstChild.InnerText.Trim();
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
            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();

            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode main = doc.GetElementbyId("main");
            List<HtmlNode> trList = main.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trList)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                if (td != null)
                {
                    HtmlNode a = td.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                    string name = a.FirstChild.InnerText.Trim();
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
            int index = 1;
            List<Dictionary<string, string>> pageList = new List<Dictionary<string, string>>();
            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> mangaContainerCls = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Equals("manga-container")).ToList();
            foreach (HtmlNode mc in mangaContainerCls)
            {
                List<HtmlNode> imgList = mc.Descendants().Where(x => x.Name.Equals("img")).ToList();
                foreach (HtmlNode img in imgList)
                {
                    string url = img.GetAttributeValue("src", "").Trim();

                    Match m = Regex.Match(url, ".+(&|&amp;)url=(?<ACTUAL_URL>[^&]+)", RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        url = m.Groups["ACTUAL_URL"].Value.Trim();
                    }

                    if (string.IsNullOrWhiteSpace(url) == false)
                    {
                        pageList.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", "Trang " + StringUtils.GenerateOrdinal(imgList.Count, index) },
                                { "url", Uri.UnescapeDataString(url) }
                            });

                        index++;
                    }
                }
            }
            return pageList;
        }
    }
}
