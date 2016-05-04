using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class Manga24hScript
    {
        private const string DOMAIN = "http://manga24h.com/";
        private const string FIRST_PAGE_URL = "http://manga24h.com/danhsach";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(FIRST_PAGE_URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode ul = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("pagination"));
            string liMaxPage = ul.FirstChild.Descendants().First().InnerText;
            return int.Parse(Regex.Replace(liMaxPage, "[^\\d]+", ""));
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();
            
            string listUrl = pageIndex == 1 ? FIRST_PAGE_URL : String.Format("{0}/{1}", FIRST_PAGE_URL, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode table = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("table"));
            List<HtmlNode> trTags = table.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trTags)
            {
                HtmlNode tdTitle = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td") && x.GetAttributeValue("class", "").Contains("panel-update-each-item-title"));
                if (tdTitle != null)
                {
                    HtmlNode a = tdTitle.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
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

            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();
            
            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode baseEle = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name.Equals("base", StringComparison.CurrentCultureIgnoreCase));
            string domainOrBaseUrl = baseEle == null ? DOMAIN : baseEle.GetAttributeValue("href", "").Trim();
            List<HtmlNode> tables = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("chapt-table")).ToList();
            foreach (HtmlNode table in tables)
            {
                HtmlNode chaptName = table.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("chap_name"));
                if (chaptName != null)
                {
                    HtmlNode a = chaptName.Element("a");
                    string name = a.InnerText.Trim();
                    string url = UrlUtils.FixUrl(domainOrBaseUrl, a.GetAttributeValue("href", "").Trim());

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

            const string pattern = @"data\s*=\s*'(?<urls>[^']+)'";
            int index = 1;
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
