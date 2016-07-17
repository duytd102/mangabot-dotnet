using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class UpTruyenScraper : IScraper
    {
        const string DOMAIN = "http://uptruyen.com";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.UpTruyenScript";
        const MangaSite SITE = MangaSite.UPTRUYEN;

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(SITE).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new UpTruyenScript().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(SITE).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new UpTruyenScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new UpTruyenScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, SITE, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(SITE).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new UpTruyenScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new UpTruyenScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, SITE, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(SITE).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new UpTruyenScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new UpTruyenScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(SITE, results);
        }
    }
}
