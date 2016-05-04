using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class IzMangaScript
    {
        private const string BASE_LIST_URL = "http://izmanga.com/danh-sach-truyen?type=new&category=all&alpha=all&page={0}&state=all&group=all";

        public int GetTotalPages()
        {
            string pattern = @".+?page=(?<PAGE_INDEX>\d+?)&.*?";
            string src = HttpUtils.MakeHttpGet(String.Format(BASE_LIST_URL, 1));
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.GetAttributeValue("class", "").Contains("phan-trang"));

            if (paginator != null)
            {
                HtmlNode lastPage = paginator.Descendants().LastOrDefault(x => x.Name.Equals("a"));
                Match p = Regex.Match(lastPage.GetAttributeValue("href", ""), pattern, RegexOptions.IgnoreCase);
                return int.Parse(p.Groups["PAGE_INDEX"].Value);
            }

            return 1;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string listUrl = String.Format(BASE_LIST_URL, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> list = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("list-truyen-item-wrap")).ToList();
            foreach (HtmlNode m in list)
            {
                HtmlNode h3 = m.Descendants().FirstOrDefault(x => x.Name.Equals("h3"));
                HtmlNode a = h3.Element("a");
                
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

            HtmlNode container = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.GetAttributeValue("class", "").Contains("chapter-list"));
            List<HtmlNode> list = container.Descendants().Where(
                x => x.GetAttributeValue("class", "").Contains("row")).ToList();
            foreach (HtmlNode c in list)
            {
                HtmlNode title = c.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                string name = title.InnerText.Trim();
                string url = title.GetAttributeValue("href", "").Trim();

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
            const string pattern = @"data\s*=\s*'(?<urls>[^']+)'";
            
            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> scriptTags = doc.DocumentNode.Descendants().Where(x => x.Name.Equals("script")).ToList();
            foreach (HtmlNode script in scriptTags)
            {
                Match imageData = Regex.Match(script.InnerHtml, pattern, RegexOptions.IgnoreCase);
                string urls = imageData.Groups["urls"].Value;
                if (string.IsNullOrEmpty(urls) == false)
                {
                    string[] ua = urls.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string u in ua)
                    {
                        if (string.IsNullOrWhiteSpace(u) == false)
                        {
                            pageList.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", "Trang " + StringUtils.GenerateOrdinal(ua.Length, index) },
                                { "url", u }
                            });
                        }
                        index++;
                    }
                }
            }

            return pageList;
        }
    }
}
