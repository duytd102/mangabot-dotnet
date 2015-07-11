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

namespace WebScraper.Scrapers.Implement
{
    class HocVienTruyenTranhScraper : IScraper
    {
        private const String BASE_LIST_URL = "http://truyen.academyvn.com/manga/all?page=";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);
            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("pagination"));
            if (paginator != null)
            {
                HtmlNode a = paginator.Descendants().Last(x => x.Name.Equals("a") && x.GetAttributeValue("rel", "").Length == 0);
                return int.Parse(a.InnerText);
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

            HtmlNode main = doc.GetElementbyId("main");
            List<HtmlNode> trList = main.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trList)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                if (td != null)
                {
                    HtmlNode a = td.Descendants().First(x => x.Name.Equals("a"));

                    Manga manga = new Manga();
                    manga.ID = Guid.NewGuid().ToString();
                    manga.Name = WebUtility.HtmlDecode(a.FirstChild.InnerText.Trim());
                    manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    manga.Site = MangaSite.HOCVIENTRUYENTRANH;

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

            HtmlNode main = doc.GetElementbyId("main");
            List<HtmlNode> trList = main.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trList)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                if (td != null)
                {
                    HtmlNode a = td.Descendants().First(x => x.Name.Equals("a"));

                    Chapter chapter = new Chapter();
                    chapter.ID = Guid.NewGuid().ToString();
                    chapter.Name = WebUtility.HtmlDecode(a.FirstChild.InnerText.Trim());
                    chapter.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    chapter.Site = MangaSite.HOCVIENTRUYENTRANH;

                    if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                        continue;

                    chapterList.Add(chapter);
                }
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            int index = 1;
            List<Page> pageList = new List<Page>();
            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> mangaContainerCls = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Equals("manga-container")).ToList();
            foreach (HtmlNode mc in mangaContainerCls)
            {
                List<HtmlNode> imgList = mc.Descendants().Where(x => x.Name.Equals("img")).ToList();
                foreach (HtmlNode img in imgList)
                {
                    Page page = new Page();
                    page.ID = Guid.NewGuid().ToString();
                    page.Name = "Trang " + StringUtils.GenerateOrdinal(imgList.Count, index);
                    page.Url = WebUtility.HtmlDecode(img.GetAttributeValue("src", "").Trim()); ;
                    page.Site = MangaSite.HOCVIENTRUYENTRANH;
                    pageList.Add(page);
                    index++;
                }
            }
            return pageList;
        }
    }
}
