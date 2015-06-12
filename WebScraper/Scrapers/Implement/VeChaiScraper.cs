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
    class VeChaiScraper : IScraper
    {
        private const String DOMAIN = "http://vechai.info/";
        private const String ROOT_LIST = "http://vechai.info/danh-sach.tall.p{0}.json";

        public int GetTotalPages()
        {
            return 300;
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
            string listUrl = String.Format(ROOT_LIST, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode ul = doc.GetElementbyId("comic-list");
            List<HtmlNode> liTags = ul.Descendants().Where(x => x.Name.Equals("li")).ToList();
            foreach (HtmlNode li in liTags)
            {
                HtmlNode a = li.Descendants().FirstOrDefault(x => x.Name.Equals("a") && x.GetAttributeValue("class", "").Contains("StoryName"));
                if (a != null)
                {
                    string name = a.InnerText.Trim();
                    if (String.IsNullOrWhiteSpace(name))
                    {
                        HtmlNode sibling = a.NextSibling.NextSibling;
                        name = sibling.InnerText.Trim();
                    }

                    Manga manga = new Manga();
                    manga.ID = Guid.NewGuid().ToString();
                    manga.Name = WebUtility.HtmlDecode(name);
                    manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    manga.Site = MangaSite.VECHAI;

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

            HtmlNode ul = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "").Contains("accordion"));
            List<HtmlNode> ulTags = ul.Descendants().Where(x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("List")).ToList();
            if (ulTags.Count > 0) ulTags.RemoveAt(0);
            foreach (HtmlNode ulTag in ulTags)
            {
                HtmlNode a = ulTag.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                if (a != null)
                {
                    Chapter chapter = new Chapter();
                    chapter.ID = Guid.NewGuid().ToString();
                    chapter.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    chapter.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    chapter.Site = MangaSite.VECHAI;

                    if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                        continue;

                    chapterList.Add(chapter);
                }
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            //const String DOMAIN_DOCTRUYEN = "http://doctruyen.vechai.info/";
            //const String DOMAIN_VECHAI = "http://vechai.info/";

            //if (chapterUrl.IndexOf(DOMAIN_DOCTRUYEN) >= 0)
            //    return GetPageListFromDocTruyen(chapterUrl);

            //if (chapterUrl.IndexOf(DOMAIN_VECHAI) >= 0)
            //    return GetPageListFromVeChai(chapterUrl);

            //return GetPageListFromBlogger(chapterUrl);


            List<Page> pageList = new List<Page>();
            int index = 1;

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode container = doc.GetElementbyId("contentChapter");
            List<HtmlNode> imgTags = container.Descendants().Where(x => x.Name.Equals("img")).ToList();
            foreach (HtmlNode img in imgTags)
            {
                Page page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(imgTags.Count, index);
                page.Url = WebUtility.HtmlDecode(img.GetAttributeValue("src", "").Trim());
                page.Site = MangaSite.VECHAI;

                if (String.IsNullOrEmpty(page.Url))
                    continue;

                pageList.Add(page);
                index++;
            }
            return pageList;
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