using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WebScraper.Utils;

namespace WebScraper.Scrapers.Scripts
{
    public class KissMangaScript
    {
        private const string FIRST_PAGE_URL = "http://kissmanga.com/MangaList?page=";

        public int GetTotalPages()
        {
            //var httpContent = new HttpRequestMessage(HttpMethod.Get, FIRST_PAGE_URL + 1);
            ////httpContent.Content = new FormUrlEncodedContent(parameters);

            //var cfHandler = new CloudflareHttpHandler();
            //HttpClient cl = new HttpClient(cfHandler);
            //Task<HttpResponseMessage> t = cl.SendAsync(httpContent);
            //t.Wait();
            //HttpResponseMessage ms = t.Result;
            //string resss = ms.Content.ReadAsStringAsync().ToString();


            string res = new CloudflareHttpUtils().Get(FIRST_PAGE_URL + 1, null);

            return 0;

            //string src = HttpUtils.MakeHttpGet(FIRST_PAGE_URL + 1);
            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(src);

            //HtmlNode ul = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("pager"));
            //if (ul == null) return 1;

            //string href = ul.LastChild.Descendants().First(x => x.Name.Equals("a")).GetAttributeValue("href", "");
            //return int.Parse(Regex.Replace(href, "[^\\d]+", ""));
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();

            string src = HttpUtils.MakeHttpGet(string.Format("{0}{1}", FIRST_PAGE_URL, pageIndex));
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode table = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("listing"));
            List<HtmlNode> trTags = table.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trTags)
            {
                HtmlNode tdTitle = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
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

            HtmlNode table = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("listing"));
            List<HtmlNode> trTags = table.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trTags)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                if (td != null)
                {
                    HtmlNode a = td.Element("a");
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

            const string pattern = "lstImages.push\\([\"|'](?<URL>.+?)[\"|']\\)";
            int index = 1;
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
