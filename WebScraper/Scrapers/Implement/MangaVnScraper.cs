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
    class MangaVnScraper : IScraper
    {
        private const String DOMAIN = "http://www.mangavn.net/";
        private const String FIRST_PAGE_URL = "http://www.mangavn.net/f3-danh-sach-truyen";
        private const int MAX_MANGA_ON_PAGE = 50;
        private const int MAX_CHAPTERS_ON_PAGE = 50;

        public int GetTotalPages()
        {
            string pagingSrc = HttpUtils.MakeHttpGet(FIRST_PAGE_URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pagingSrc);
            return GetMaxPage(doc.GetElementbyId("main-content"));
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            string pageUrl = "";
            if (pageIndex == 1)
            {
                pageUrl = FIRST_PAGE_URL;
            }
            else
            {
                pageUrl = String.Format("{0}f3p{1}-danh-sach-truyen", DOMAIN, pageIndex * MAX_MANGA_ON_PAGE);
            }

            Manga manga;
            List<Manga> mangaList = new List<Manga>();
            string pageSrc = HttpUtils.MakeHttpGet(pageUrl);
            HtmlDocument pageDoc = new HtmlDocument();
            pageDoc.LoadHtml(pageSrc);
            List<HtmlNode> mangaBlocks = pageDoc.GetElementbyId("main-content").Descendants()
                .Where(x => x.GetAttributeValue("class", "").Contains("manga_load")).ToList();

            foreach (HtmlNode mangaBlk in mangaBlocks)
            {
                HtmlNode a = mangaBlk.Descendants()
                    .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("blog_message")).Descendants()
                    .FirstOrDefault(x => x.Name.Equals("a"));

                string name = WebUtility.HtmlDecode(a.InnerText.Trim());
                string url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());

                manga = new Manga();
                manga.ID = Guid.NewGuid().ToString();
                manga.Name = name;
                manga.Url = url;
                manga.Site = MangaSite.MANGAVN;
                mangaList.Add(manga);
            }

            return mangaList;
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Chapter> chapterList = new List<Chapter>();
            Chapter chapter;

            string chapterCode = GetChapterCode(mangaUrl);
            string chapterPathName = GetChapterPathName(mangaUrl);

            string mangaSrc = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument mangaDoc = new HtmlDocument();
            mangaDoc.LoadHtml(mangaSrc);

            HtmlNode blogComments = mangaDoc.GetElementbyId("blog_comments");
            int totalPage = GetMaxPage(blogComments);

            for (int i = 1; i <= totalPage;i++ )
            {
                try
                {
                    if (i > 1)
                    {
                        string pUrl = HttpUtils.CombineUrl(DOMAIN,
                            String.Format("{0}p{1}{2}", chapterCode, (i - 1) * MAX_CHAPTERS_ON_PAGE, chapterPathName));
                        mangaSrc = HttpUtils.MakeHttpGet(pUrl);
                        mangaDoc = new HtmlDocument();
                        mangaDoc.LoadHtml(mangaSrc);

                        blogComments = mangaDoc.GetElementbyId("blog_comments");
                    }

                    List<HtmlNode> trTags = blogComments.Descendants()
                        .FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("listing")).Descendants()
                        .Where(x => x.Name.Equals("tr")).ToList();

                    foreach (HtmlNode tr in trTags)
                    {
                        string name = "", url = "", date = "";

                        List<HtmlNode> tdTags = tr.Descendants().Where(x => x.Name.Equals("td")).ToList();
                        if (tdTags.Count > 0)
                        {
                            HtmlNode a = tdTags[0].Element("a");
                            name = WebUtility.HtmlDecode(a.InnerText.Trim());
                            url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                        }

                        if (tdTags.Count > 1)
                        {
                            date = tdTags[1].InnerText.Trim();
                        }

                        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(url))
                            continue;

                        chapter = new Chapter();
                        chapter.ID = Guid.NewGuid().ToString();
                        chapter.Name = name;
                        chapter.Url = url;
                        chapter.PublishedDate = date;
                        chapter.Site = MangaSite.MANGAVN;
                        chapterList.Add(chapter);
                    }
                }
                catch { }
            }

            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Page> pageList = new List<Page>();
            Page page;
            int index = 1;

            string chapterSrc = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument chapterDoc = new HtmlDocument();
            chapterDoc.LoadHtml(chapterSrc);

            List<HtmlNode> imgTags = chapterDoc.GetElementbyId("main-content").Descendants()
                .FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("img_truyen")).Descendants()
                .Where(x => x.Name.Equals("img")).ToList();

            foreach (HtmlNode img in imgTags)
            {
                page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(imgTags.Count, index);
                page.Url = WebUtility.HtmlDecode(img.GetAttributeValue("src", "").Trim()); ;
                page.Site = MangaSite.MANGAVN;
                pageList.Add(page);
                index++;
            }

            return pageList;
        }

        private int GetMaxPage(HtmlNode node)
        {
            HtmlNode paginator = node.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("paging"));
            if (paginator == null)
                return 1;

            List<HtmlNode> indexes = paginator.Descendants().Where(x => x.Name.Equals("a")).ToList();

            if (indexes.Count < 2)
                return 1;

            return int.Parse(indexes[indexes.Count - 2].InnerText);
        }

        private string GetChapterCode(string mangaUrl)
        {
            Uri u = new Uri(mangaUrl);
            string path = u.LocalPath;
            int dashIndex = path.IndexOf("-");
            if (dashIndex > 0)
                return path.Substring(0, dashIndex);
            else
                return "";
        }

        private string GetChapterPathName(string mangaUrl)
        {
            Uri u = new Uri(mangaUrl);
            string path = u.LocalPath;
            int dashIndex = path.IndexOf("-");
            if (dashIndex > 0)
                return path.Substring(dashIndex);
            else
                return "";
        }
    }
}