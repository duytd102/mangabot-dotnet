using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Utils;

namespace WebScraper.Scrapers.Implement
{
    class IzMangaScraper : IScraper
    {
        private const String BASE_LIST_URL = "http://izmanga.com/danh_sach_truyen?type=new&category=all&alpha=all&state=all&group=all&page=";

        public int GetTotalPages()
        {
            string pattern = @".+?page=(?<PAGE_INDEX>\d+?)&.*?";
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.GetAttributeValue("class", "").Contains("phan-trang"));

            if (paginator != null)
            {
                HtmlNode lastPage = paginator.Descendants().Last(x => x.Name.Equals("a"));
                Match p = Regex.Match(lastPage.GetAttributeValue("href", ""), pattern, RegexOptions.IgnoreCase);
                return int.Parse(p.Groups["PAGE_INDEX"].Value);
            }

            return 1;
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
            string listUrl = String.Format("{0}{1}", BASE_LIST_URL, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> list = doc.DocumentNode.Descendants().Where(
                x => x.GetAttributeValue("class", "").Contains("list-truyen-item-wrap")).ToList();

            foreach (HtmlNode m in list)
            {
                HtmlNode h3 = m.Descendants().FirstOrDefault(x => x.Name.Equals("h3"));
                HtmlNode a = h3.Element("a");

                Manga manga = new Manga();
                manga.ID = Guid.NewGuid().ToString();
                manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                manga.Site = MangaSite.IZMANGA;

                if (String.IsNullOrEmpty(manga.Name) || String.IsNullOrEmpty(manga.Url))
                    continue;

                mangaList.Add(manga);
            }

            return mangaList;
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Chapter> chapterList = new List<Chapter>();
            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode container = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.GetAttributeValue("class", "").Contains("chapter-list"));
            List<HtmlNode> list = container.Descendants().Where(
                x => x.GetAttributeValue("class", "").Contains("row")).ToList();
            foreach (HtmlNode c in list)
            {
                HtmlNode title = c.Descendants().First(x => x.Name.Equals("a"));

                Chapter chapter = new Chapter();
                chapter.ID = Guid.NewGuid().ToString();
                chapter.Name = WebUtility.HtmlDecode(title.InnerText.Trim());
                chapter.Url = WebUtility.HtmlDecode(title.GetAttributeValue("href", "").Trim());
                chapter.Site = MangaSite.IZMANGA;

                if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                    continue;

                chapterList.Add(chapter);
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            const string pattern = @"data\s*=\s*'(?<urls>[^']+)'";
            int index = 1;
            List<Page> pageList = new List<Page>();
            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> scriptTags = doc.DocumentNode.Descendants().Where(x => x.Name.Equals("script")).ToList();
            foreach (HtmlNode script in scriptTags)
            {
                Match imageData = Regex.Match(script.InnerHtml, pattern, RegexOptions.IgnoreCase);
                string urls = imageData.Groups["urls"].Value;
                if (!String.IsNullOrEmpty(urls))
                {
                    string[] ua = urls.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string u in ua)
                    {
                        Page page = new Page();
                        page.ID = Guid.NewGuid().ToString();
                        page.Name = "Trang " + StringUtils.GenerateOrdinal(ua.Length, index);
                        page.Url = WebUtility.HtmlDecode(u);
                        page.Site = MangaSite.MANGA24H;

                        if (String.IsNullOrEmpty(page.Url))
                            continue;

                        pageList.Add(page);
                        index++;
                    }
                }
            }
            return pageList;
        }
    }
}
