using Common;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using WebScraper.Data;

namespace WebScraper.Scrapers
{
    class DictionaryToList
    {
        public static List<Manga> ToMangaList(string DOMAIN, MangaSite site, List<Dictionary<string, string>> results)
        {
            List<Manga> mangaList = new List<Manga>();
            foreach (Dictionary<string, string> dic in results)
            {
                Manga manga = new Manga();
                manga.ID = Guid.NewGuid().ToString();
                manga.Name = WebUtility.HtmlDecode(dic["name"]);
                manga.Url = WebUtility.HtmlDecode(UrlUtils.FixUrl(DOMAIN, dic["url"]));
                manga.Site = site;
                mangaList.Add(manga);
            }
            return mangaList;
        }

        public static List<Chapter> ToChapterList(string DOMAIN, MangaSite site, List<Dictionary<string, string>> results)
        {
            List<Chapter> chapterList = new List<Chapter>();
            foreach (Dictionary<string, string> dic in results)
            {
                Chapter chapter = new Chapter();
                chapter.ID = Guid.NewGuid().ToString();
                chapter.Name = WebUtility.HtmlDecode(dic["name"]);
                chapter.Url = WebUtility.HtmlDecode(UrlUtils.FixUrl(DOMAIN, dic["url"]));
                chapter.Site = site;
                chapterList.Add(chapter);
            }
            return chapterList;
        }

        public static List<Page> ToPageList(MangaSite site, List<Dictionary<string, string>> results)
        {
            List<Page> pageList = new List<Page>();
            int index = 1;
            foreach (Dictionary<string, string> dic in results)
            {
                Page page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(results.Count, index);
                page.Url = dic["url"];
                page.Site = site;
                pageList.Add(page);
                index++;
            }
            return pageList;
        }
    }
}
