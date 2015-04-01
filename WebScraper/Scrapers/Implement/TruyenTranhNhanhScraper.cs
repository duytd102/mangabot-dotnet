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
    class TruyenTranhNhanhScraper : IScraper
    {
        private const String BASE_LIST_URL = "http://truyentranhnhanh.com/comics/list/truyenmoi/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + "1");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode paginator = doc.DocumentNode.Descendants().First(
                x => x.GetAttributeValue("class", "").Contains("next-page"));
            HtmlNode lastPageIndex = paginator.Descendants().Last(x => x.Name.Equals("a"));
            string url = lastPageIndex.GetAttributeValue("href", "");
            return int.Parse(url.Replace(BASE_LIST_URL, ""));
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + pageIndex);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode container = doc.GetElementbyId("mainle");
            List<HtmlNode> list = container.Descendants().Where(
                x => x.GetAttributeValue("class", "").Contains("title")).ToList();

            foreach (HtmlNode m in list)
            {
                HtmlNode a = m.Descendants().First(x => x.Name.Equals("a"));

                Manga manga = new Manga();
                manga.ID = Guid.NewGuid().ToString();
                manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                manga.Site = MangaSite.TRUYENTRANHNHANH;

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

            HtmlNode container = doc.DocumentNode.Descendants().First(
                x => x.GetAttributeValue("class", "").Contains("detail_list"));

            List<HtmlNode> liTags = container.Descendants().Where(x => x.Name.Equals("li")).ToList();

            foreach (HtmlNode li in liTags)
            {
                HtmlNode a = li.Descendants().First(x => x.Name.Equals("a"));

                Chapter chapter = new Chapter();
                chapter.ID = Guid.NewGuid().ToString();
                chapter.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                chapter.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                chapter.Site = MangaSite.TRUYENTRANHNHANH;

                if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                    continue;

                chapterList.Add(chapter);
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Page> pageList = new List<Page>();
            int index = 1;

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode slide = doc.DocumentNode.Descendants().First(
                x => x.GetAttributeValue("class", "").Contains("neoslideshow"));
            List<HtmlNode> imgTags = slide.Descendants().Where(x => x.Name.Equals("img")).ToList();

            foreach (HtmlNode img in imgTags)
            {
                Page page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(imgTags.Count, index);
                page.Url = WebUtility.HtmlDecode(img.GetAttributeValue("src", "").Trim());
                page.Site = MangaSite.TRUYENTRANHNHANH;

                if (String.IsNullOrEmpty(page.Url))
                    continue;

                pageList.Add(page);
                index++;
            }

            return pageList;
        }
    }
}
