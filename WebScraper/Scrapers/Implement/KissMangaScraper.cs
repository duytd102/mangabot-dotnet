using Common;
using Common.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebScraper.Data;

namespace WebScraper.Scrapers.Implement
{
    public class KissMangaScraper : IScraper
    {
        private const String DOMAIN = "http://kissmanga.com";
        private const String FIRST_PAGE_URL = "http://kissmanga.com/MangaList?page=";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(FIRST_PAGE_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode ul = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("pager"));
            if (ul == null) return 1;

            string href = ul.LastChild.Descendants().First(x=>x.Name.Equals("a")).GetAttributeValue("href", "");
            return int.Parse(Regex.Replace(href, "[^\\d]+", ""));
        }

        public List<Data.Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
            string listUrl = String.Format("{0}{1}", FIRST_PAGE_URL, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
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

                    Manga manga = new Manga();
                    manga.ID = Guid.NewGuid().ToString();
                    manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    manga.Url = DOMAIN + WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    manga.Site = MangaSite.KISSMANGA;

                    if (String.IsNullOrEmpty(manga.Name) || String.IsNullOrEmpty(manga.Url))
                        continue;

                    mangaList.Add(manga);
                }
            }
            return mangaList;
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Chapter> chapterList = new List<Chapter>();
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

                    Chapter chapter = new Chapter();
                    chapter.ID = Guid.NewGuid().ToString();
                    chapter.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    chapter.Url = DOMAIN + WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    chapter.Site = MangaSite.KISSMANGA;

                    if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                        continue;

                    chapterList.Add(chapter);
                }
            }
            return chapterList;
        }

        public List<Data.Page> GetPageList(string chapterUrl)
        {
            const string pattern = "lstImages.push\\([\"|'](?<URL>.+?)[\"|']\\)";
            List<Page> pageList = new List<Page>();
            int index = 1;

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            MatchCollection list = Regex.Matches(src, pattern, RegexOptions.IgnoreCase);
            foreach (Match img in list)
            {
                string url = img.Groups["URL"].Value.Trim();
                Page page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(list.Count, index);
                page.Url = WebUtility.HtmlDecode(url);
                page.Site = MangaSite.KISSMANGA;

                if (String.IsNullOrEmpty(page.Url))
                    continue;

                pageList.Add(page);
                index++;
            }
            return pageList;
        }
    }
}
