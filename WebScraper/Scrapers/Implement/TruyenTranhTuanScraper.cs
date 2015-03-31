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
    class TruyenTranhTuanScraper : IScraper
    {
        private const String LIST_URL = "http://truyentranhtuan.com/danh-sach-truyen";

        public int GetTotalPages()
        {
            return 1;
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
            string src = HttpUtils.MakeHttpGet(LIST_URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode container = doc.GetElementbyId("new-chapter");
            List<HtmlNode> list = container.Descendants().Where(
                x => x.GetAttributeValue("class", "").Contains("manga-focus")).ToList();

            foreach (HtmlNode m in list)
            {
                HtmlNode a = m.Descendants().First(x => x.GetAttributeValue("class", "").Contains("manga")).FirstChild;

                Manga manga = new Manga();
                manga.ID = Guid.NewGuid().ToString();
                manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                manga.Site = MangaSite.TRUYENTRANHTUAN;

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

            HtmlNode container = doc.GetElementbyId("manga-chapter");
            List<HtmlNode> list = container.Descendants().Where(
                x => x.GetAttributeValue("class", "").Contains("chapter-name")).ToList();

            foreach (HtmlNode c in list)
            {
                HtmlNode a = c.FirstChild;

                Chapter chapter = new Chapter();
                chapter.ID = Guid.NewGuid().ToString();
                chapter.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                chapter.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                chapter.Site = MangaSite.TRUYENTRANHTUAN;

                if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                    continue;

                chapterList.Add(chapter);
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            const string pattern = @"slides_page_url_path\s*=\s*\[\s*(?<urls>[^\]]+)\]";
            List<Page> pageList = new List<Page>();
            int index = 1;
            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            Match arr = Regex.Match(src, pattern);
            string grp = arr.Groups["urls"].Value;
            string[] list = grp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string u in list )
            {
                Page page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(list.Length, index);
                page.Url = WebUtility.HtmlDecode(u.Replace("\"", "").Replace("'", "").Trim());
                page.Site = MangaSite.TRUYENTRANHTUAN;

                if (String.IsNullOrEmpty(page.Url))
                    continue;

                pageList.Add(page);
                index++;
            }
            return pageList;
        }
    }
}
