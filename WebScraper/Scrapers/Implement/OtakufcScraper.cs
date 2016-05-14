using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class OtakufcScraper : IScraper
    {
        private const string DOMAIN = "http://otakufc.com/";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.OtakufcScript";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.OTAKUFC).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new OtakufcScript().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.OTAKUFC).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new OtakufcScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new OtakufcScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.OTAKUFC, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.OTAKUFC).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new OtakufcScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new OtakufcScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.OTAKUFC, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.OTAKUFC).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new OtakufcScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new OtakufcScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.OTAKUFC, results);
        }
    }
}
