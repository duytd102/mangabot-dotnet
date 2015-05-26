using Common;
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
    class VeChaiScraper : IScraper
    {
        private const String DOMAIN = "http://vechai.info/";
        private const String TOTAL_PAGES_URL = "http://vechai.info/danh-sach/";
        private const String ROOT_LIST = "http://vechai.info/list.php?job=ajaxlist&letter=all&sort=1&page=";

        public int GetTotalPages()
        {
            String idContentPattern = "<div[^>]*?id\\s*=\\s*[\"|']\\s*content\\s*[\"|'].*?>.*?<div[^>]*?id\\s*=\\s*[\"|']\\s*page-footer\\s*[\"|'].*?>";
            String paginationPattern = "<div[^>]*?class\\s*=\\s*[\"|']\\s*list-manga-paging\\s*[\"|'].*?>.*?</div>";
            String pageIndexPattern = "<span[^>]*?>(?<PAGE_INDEX>.*?)</span>";
            String listSrc = HttpUtils.MakeHttpGet(TOTAL_PAGES_URL);
            Match idContentMatch = Regex.Match(listSrc, idContentPattern);
            Match paginationMatch = Regex.Match(idContentMatch.Value, paginationPattern);
            MatchCollection pageIndexesMatchCollection = Regex.Matches(paginationMatch.Value, pageIndexPattern);
            if (pageIndexesMatchCollection.Count == 0) return 0;
            Match lastPageIndexMatch = pageIndexesMatchCollection[pageIndexesMatchCollection.Count - 1];
            return int.Parse(lastPageIndexMatch.Groups["PAGE_INDEX"].Value);
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            String trPattern = "<tr[^>]*?>.*?</tr>";
            String tdPattern = "<td[^>]*?>.*?</td>";
            String aPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<MANGA_URL>.*?)[\"|'].*?>(?<MANGA_NAME>.*?)</a>";
            List<Manga> mangaList = new List<Manga>();
            Manga manga;
            String url;
            String mangaListUrl = String.Format("{0}{1}", ROOT_LIST, pageIndex);
            String mangaListSrc = HttpUtils.MakeHttpGet(mangaListUrl);
            MatchCollection trMatchCollection = Regex.Matches(mangaListSrc, trPattern);
            for (int i = 1; i < trMatchCollection.Count; i++)
            {
                MatchCollection tdMatchCollection = Regex.Matches(trMatchCollection[i].Value, tdPattern);
                Match firstTdMatch = tdMatchCollection[0];
                Match aMatch = Regex.Match(firstTdMatch.Value, aPattern);

                url = WebUtility.HtmlDecode(aMatch.Groups["MANGA_URL"].Value.Trim());
                url = HttpUtils.CombineUrl(DOMAIN, url);

                manga = new Manga();
                manga.ID = Guid.NewGuid().ToString();
                manga.Name = WebUtility.HtmlDecode(aMatch.Groups["MANGA_NAME"].Value.Trim());
                manga.Url = url;
                manga.Site = MangaSite.VECHAI;
                mangaList.Add(manga);
            }
            return mangaList;
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            String chapterListPattern = "<div[^>]*?id\\s*=\\s*[\"|']\\s*zoomtext\\s*[\"|'].*?>.*?<div[^>]*?id\\s*=\\s*[\"|']\\s*commentWrapper\\s*[\"|'].*?>";
            String pListPattern = "<p[^>]*?>.*?</p>";
            String aPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<CHAPTER_URL>.*?)[\"|'].*?>(?<CHAPTER_NAME>.*?)</a>";
            List<Chapter> chapterList = new List<Chapter>();
            Chapter chapter;
            String name, url;
            String mangaSrc = HttpUtils.MakeHttpGet(mangaUrl);
            Match chapterListMatch = Regex.Match(mangaSrc, chapterListPattern);
            MatchCollection pListMatchCollection = Regex.Matches(chapterListMatch.Value, pListPattern);
            foreach (Match pMatch in pListMatchCollection)
            {
                MatchCollection aMatchCollection = Regex.Matches(pMatch.Value, aPattern);
                foreach (Match aMatch in aMatchCollection)
                {
                    name = WebUtility.HtmlDecode(aMatch.Groups["CHAPTER_NAME"].Value.Trim());
                    name = RemoveHtmlTag(name);
                    if (name.IndexOf("<img") >= 0) continue;

                    url = aMatch.Groups["CHAPTER_URL"].Value.Trim();
                    if (url.Length == 0) continue;
                    url = WebUtility.HtmlDecode(url);

                    chapter = new Chapter();
                    chapter.ID = Guid.NewGuid().ToString();
                    chapter.Name = name;
                    chapter.Url = url;
                    chapter.Site = MangaSite.VECHAI;
                    chapterList.Add(chapter);
                }
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            const String DOMAIN_DOCTRUYEN = "http://doctruyen.vechai.info/";
            const String DOMAIN_VECHAI = "http://vechai.info/";

            if (chapterUrl.IndexOf(DOMAIN_DOCTRUYEN) >= 0)
                return GetPageListFromDocTruyen(chapterUrl);

            if (chapterUrl.IndexOf(DOMAIN_VECHAI) >= 0)
                return GetPageListFromVeChai(chapterUrl);

            return GetPageListFromBlogger(chapterUrl);
        }

        private List<Page> GetPageListFromBlogger(String chapterUrl)
        {
            String contentWrapperPattern = "<div[^>]*?id\\s*=\\s*[\"|']\\s*content-wrapper\\s*[\"|'].*?>.*?<div[^>]*?id\\s*=\\s*[\"|']\\s*footer\\s*[\"|'].*?>";
            String imageListPattern = "<div[^>]*?class\\s*=\\s*[\"|']\\s*post-body\\sentry-content\\s*[\"|'].*?>.*?<div[^>]*?class\\s*=\\s*[\"|']\\s*post-footer\\s*[\"|'].*?>";
            String imagePattern = "<img[^>]*?src\\s*=\\s*[\"|'](?<PAGE_URL>.*?)[\"|'].*?>";
            List<Page> pageList = new List<Page>();
            Page page;
            int count = 1;
            String url;
            String pageListSrc = HttpUtils.MakeHttpGet(chapterUrl);
            Match contentMatch = Regex.Match(pageListSrc, contentWrapperPattern);
            Match imageListMatch = Regex.Match(contentMatch.Value, imageListPattern);
            MatchCollection imageMatchCollection = Regex.Matches(imageListMatch.Value, imagePattern);
            foreach (Match imageMatch in imageMatchCollection)
            {
                url = imageMatch.Groups["PAGE_URL"].Value.Trim();
                url = WebUtility.HtmlDecode(url);

                page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = String.Format("Trang {0}", StringUtils.GenerateOrdinal(imageMatchCollection.Count, count));
                page.Url = url;
                page.Site = MangaSite.VECHAI;
                pageList.Add(page);
                count++;
            }
            return pageList;
        }

        private List<Page> GetPageListFromDocTruyen(String chapterUrl)
        {
            String entryPattern = "<div[^>]*?class\\s*=\\s*[\"|']\\s*entry\\s*[\"|'].*?>.*?<[^>]*?class\\s*=\\s*[\"|']\\s*related_post_title\\s*[\"|'].*?>";
            String imagePattern = "<img[^>]*?src\\s*=\\s*[\"|'](?<PAGE_URL>.*?)[\"|'].*?>";
            List<Page> pageList = new List<Page>();
            Page page;
            int count = 1;
            String url;
            String pageListSrc = HttpUtils.MakeHttpGet(chapterUrl);
            Match entryMatch = Regex.Match(pageListSrc, entryPattern);
            MatchCollection imageMatchCollection = Regex.Matches(entryMatch.Value, imagePattern);
            foreach (Match imageMatch in imageMatchCollection)
            {
                url = imageMatch.Groups["PAGE_URL"].Value.Trim();
                url = WebUtility.HtmlDecode(url);

                page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = String.Format("Trang {0}", StringUtils.GenerateOrdinal(imageMatchCollection.Count, count));
                page.Url = url;
                page.Site = MangaSite.VECHAI;
                pageList.Add(page);
                count++;
            }
            return pageList;
        }

        private List<Page> GetPageListFromVeChai(String chapterUrl)
        {
            String pageListPattern = "<div[^>]*?id\\s*=\\s*[\"|']\\s*zoomtext\\s*[\"|'].*?>.*?<div[^>]*?id\\s*=\\s*[\"|']\\s*commentWrapper\\s*[\"|'].*?>";
            String aPattern = "<a[^>]*?href\\s*=\\s*[\"|'](?<PAGE_URL>.*?)[\"|'].*?>(?<PAGE_IMAGE>.*?)</a>";
            List<Page> pageList = new List<Page>();
            Page page;
            int count = 1;
            String url;
            String pageSrc = HttpUtils.MakeHttpGet(chapterUrl);
            Match pageMatch = Regex.Match(pageSrc, pageListPattern);
            MatchCollection aMatchCollection = Regex.Matches(pageMatch.Value, aPattern);
            foreach (Match aMatch in aMatchCollection)
            {
                url = aMatch.Groups["PAGE_URL"].Value.Trim();
                url = WebUtility.HtmlDecode(url);

                page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = String.Format("Trang {0}", StringUtils.GenerateOrdinal(aMatchCollection.Count, count));
                page.Url = url;
                page.Site = MangaSite.VECHAI;
                pageList.Add(page);
                count++;
            }
            return pageList;
        }

        private String RemoveHtmlTag(String name)
        {
            String htmlTagPattern = "<(?<TAG>\\w+)[^>]*?>(?<TEXT>.*?)</\\k<TAG>>";
            String fixedName = name;
            String htmlTag;
            Match htmlMatch;
            int counter = 0;
            int maxCounter = name.Length;
            do
            {
                counter++;
                htmlMatch = Regex.Match(fixedName, htmlTagPattern);
                htmlTag = htmlMatch.Groups["TAG"].Value.Trim();
                if (htmlTag.Length > 0)
                    fixedName = htmlMatch.Groups["TEXT"].Value.Trim();
            } while (htmlTag.Length > 0 && counter < maxCounter);
            return fixedName;
        }
    }
}