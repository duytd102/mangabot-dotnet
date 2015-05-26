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
    class Manga24hScraper : IScraper
    {
        private const String DOMAIN = "http://manga24h.com/";
        private const String FIRST_PAGE_URL = "http://manga24h.com/danhsach";
        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(FIRST_PAGE_URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode ul = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("pagination"));
            string liMaxPage = ul.FirstChild.Descendants().First().InnerText;
            return int.Parse(Regex.Replace(liMaxPage, "[^\\d]+", ""));
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
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

                    Manga manga = new Manga();
                    manga.ID = Guid.NewGuid().ToString();
                    manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    manga.Url = DOMAIN + WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    manga.Site = MangaSite.MANGA24H;

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

            HtmlNode table = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("chapt-table"));
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
                    chapter.Site = MangaSite.MANGA24H;

                    if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                        continue;

                    chapterList.Add(chapter);
                }
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
