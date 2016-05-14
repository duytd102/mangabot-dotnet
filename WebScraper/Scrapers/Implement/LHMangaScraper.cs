using Common;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class LHMangaScraper : IScraper
    {
        private const string DOMAIN = "http://lhmanga.com/";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.LHMangaScript";

        int IScraper.GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.LHMANGA).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new LHMangaScript().GetTotalPages();
        }

        List<Manga> IScraper.GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.LHMANGA).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new LHMangaScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new LHMangaScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.LHMANGA, results);
        }

        List<Chapter> IScraper.GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.LHMANGA).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new LHMangaScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new LHMangaScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.LHMANGA, results);
        }

        List<Page> IScraper.GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.LHMANGA).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new LHMangaScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new LHMangaScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.LHMANGA, results);
        }
    }
}
