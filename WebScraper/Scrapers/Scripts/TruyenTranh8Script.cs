using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class TruyenTranh8Script
    {
        const string BASE_LIST_URL = "http://truyentranh8.net/danh_sach_truyen/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode tblChap = doc.GetElementbyId("tblChap");
            HtmlNode lastA = tblChap.Descendants().LastOrDefault(x => x.Name.Equals("a") && x.GetAttributeValue("data-page", null) != null);

            return lastA == null ? 1 : lastA.GetAttributeValue("data-page", 1);
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string listUrl = pageIndex > 1 ? String.Format("{0}page={1}", BASE_LIST_URL, pageIndex) : BASE_LIST_URL;
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode tblChap = doc.GetElementbyId("tblChap");
            List<HtmlNode> trTags = tblChap.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trTags)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td") && x.GetAttributeValue("class", "").Contains("tit"));

                if (td != null)
                {
                    HtmlNode a = td.Element("a");
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
            }

            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> aTags = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("itemprop", "").Contains("itemListElement")).ToList();
            foreach (HtmlNode a in aTags)
            {
                HtmlNode title = a.Descendants().FirstOrDefault(x => x.Name.Equals("h2"));
                if (title != null)
                {
                    HtmlNode nameNode = title.Descendants().FirstOrDefault(x => x.Name.Equals("strong"));
                    if (nameNode != null)
                    {
                        string name = nameNode.InnerText.Trim();
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
