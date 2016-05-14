using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class MangaParkScraper : IScraper
    {
        const string DOMAIN = "http://mangapark.me/";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.MangaParkScript";

        int IScraper.GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.MANGAPARK).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new MangaParkScript().GetTotalPages();
        }

        List<Manga> IScraper.GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.MANGAPARK).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new MangaParkScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new MangaParkScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.MANGAPARK, results);
        }

        List<Chapter> IScraper.GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.MANGAPARK).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new MangaParkScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new MangaParkScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.MANGAPARK, results);
        }

        List<Page> IScraper.GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.MANGAPARK).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new MangaParkScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new MangaParkScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.MANGAPARK, results);
        }
    }
}
