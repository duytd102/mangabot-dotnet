using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Scrapers.Scripts
{
    public class TruyenTranhNetScript
    {
        private const string ROOT_LIST = "http://truyentranh.net/danh-sach.tall.html?p=";

        public int GetTotalPages()
        {
            string listUrl = ROOT_LIST + 1;
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode pagination = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("pagination"));
            HtmlNode lastA = pagination.Descendants().LastOrDefault(x => x.Name.Equals("a"));
            string index = lastA.GetAttributeValue("href", "").Replace(ROOT_LIST, "");
            return int.Parse(index);
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();

            string listUrl = ROOT_LIST + pageIndex;
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode loadList = doc.GetElementbyId("loadlist");
            List<HtmlNode> mangaElements = loadList.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("mainpage-manga")).ToList();

            foreach (HtmlNode mangaElement in mangaElements)
            {
                HtmlNode a = mangaElement.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("media-body"))
                    .Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                string name = a.InnerText.Trim();
                string url = a.GetAttributeValue("href", "").Trim();

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(url))
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

            List<HtmlNode> chapterContainerList = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("total-chapter")).ToList();
            foreach (HtmlNode chapterContainer in chapterContainerList)
            {
                if (chapterContainer.Descendants().Count(x => x.GetAttributeValue("class", "").Contains("content")) > 0)
                {
                    List<HtmlNode> aList = chapterContainer.Descendants().Where(x => x.Name.Equals("a")).ToList();
                    foreach (HtmlNode a in aList)
                    {
                        string name = GetInnerTextWithoutElements(a);
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

            HtmlNode container = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("each-page"));
            List<HtmlNode> imgTags = container.Descendants().Where(x => x.Name.Equals("img")).ToList();
            foreach (HtmlNode img in imgTags)
            {
                string url = img.GetAttributeValue("src", "").Trim();

                if (string.IsNullOrWhiteSpace(url) == false)
                {
                    pageList.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", "Trang " + StringUtils.GenerateOrdinal(imgTags.Count, index) },
                            { "url", url }
                        });

                    index++;
                }
            }

            return pageList;
        }

        private string GetInnerTextWithoutElements(HtmlNode a)
        {
            string name = "";
            foreach (HtmlNode txtNode in a.Elements("#text"))
            {
                if (string.IsNullOrWhiteSpace(txtNode.InnerText) == false)
                {
                    name += (txtNode.InnerText.Trim() + " ");
                    break;
                }
            }
            return name.Trim();
        }
    }
}
