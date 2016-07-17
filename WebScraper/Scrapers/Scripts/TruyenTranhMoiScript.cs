using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Scrapers.Scripts
{
    public class TruyenTranhMoiScript
    {
        const string BASE_LIST_URL = "http://truyentranhmoi.com/danh-sach/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode pager = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("wp-pagenavi"));
            HtmlNode lastA = pager.Descendants().LastOrDefault(x => x.Name.Equals("a"));
            if (lastA == null)
            {
                return 1;
            }
            else
            {
                string href = lastA.GetAttributeValue("href", "");
                return int.Parse(href.Replace(BASE_LIST_URL + "page/", "").Replace("/", ""));
            }
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();

            string listUrl = pageIndex == 1 ? BASE_LIST_URL : (BASE_LIST_URL + "page/" + pageIndex + "/");
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode chaps = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("chap-list"));
            HtmlNode ul = chaps.Descendants().FirstOrDefault(x => x.Name.Equals("ul"));
            List<HtmlNode> liList = ul.Descendants().Where(x => x.Name.Equals("li")).ToList();
            foreach (HtmlNode li in liList)
            {
                HtmlNode a = li.Descendants().FirstOrDefault(x => x.Name.Equals("a") && x.GetAttributeValue("rel", "").Contains("nofollow") == false);
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

            HtmlNode chap = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("chap-list"));
            HtmlNode ul = chap.Descendants().FirstOrDefault(x => x.Name.Equals("ul"));
            List<HtmlNode> liList = ul.Descendants().Where(x => x.Name.Equals("li")).ToList();
            foreach (HtmlNode li in liList)
            {
                HtmlNode a = li.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
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

            HtmlNode list = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("image-chap"));
            List<HtmlNode> imgList = list.Descendants().Where(x => x.Name.Equals("img")).ToList();
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
