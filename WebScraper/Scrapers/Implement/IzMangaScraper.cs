using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class IzMangaScraper : IScraper
    {
        private const string DOMAIN = "http://iztruyentranh.com/";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.IzMangaScript";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.IZTRUYENTRANH).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new IzTruyenTranhScript().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.IZTRUYENTRANH).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new IzTruyenTranhScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new IzTruyenTranhScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.IZTRUYENTRANH, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.IZTRUYENTRANH).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new IzTruyenTranhScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new IzTruyenTranhScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.IZTRUYENTRANH, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.IZTRUYENTRANH).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new IzTruyenTranhScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new IzTruyenTranhScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.IZTRUYENTRANH, results);
        }
    }
}
