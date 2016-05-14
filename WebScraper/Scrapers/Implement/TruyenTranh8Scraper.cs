using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    public class TruyenTranh8Scraper : IScraper
    {
        const string DOMAIN = "http://truyentranh8.net";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.TruyenTranh8Script";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.TRUYENTRANH8).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new TruyenTranh8Script().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.TRUYENTRANH8).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new TruyenTranh8Script().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new TruyenTranh8Script().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.TRUYENTRANH8, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.TRUYENTRANH8).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new TruyenTranh8Script().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new TruyenTranh8Script().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.TRUYENTRANH8, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.TRUYENTRANH8).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new TruyenTranh8Script().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new TruyenTranh8Script().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.TRUYENTRANH8, results);
        }
    }
}
