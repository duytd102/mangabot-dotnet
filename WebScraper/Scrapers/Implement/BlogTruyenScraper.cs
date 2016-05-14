using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class BlogTruyenScraper : IScraper
    {
        const string DOMAIN = "http://blogtruyen.com";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.BlogTruyenScript";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.BLOGTRUYEN).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new BlogTruyenScript().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.BLOGTRUYEN).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new BlogTruyenScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new BlogTruyenScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.BLOGTRUYEN, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.BLOGTRUYEN).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new BlogTruyenScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new BlogTruyenScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.BLOGTRUYEN, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.BLOGTRUYEN).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new BlogTruyenScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new BlogTruyenScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.BLOGTRUYEN, results);
        }
    }
}
