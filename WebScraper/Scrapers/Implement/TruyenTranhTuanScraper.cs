using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class TruyenTranhTuanScraper : IScraper
    {
        const string DOMAIN = "http://truyentranhtuan.com/";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.TruyenTranhTuanScript";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.TRUYENTRANHTUAN).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new TruyenTranhTuanScript().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.TRUYENTRANHTUAN).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new TruyenTranhTuanScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new TruyenTranhTuanScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.TRUYENTRANHTUAN, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.TRUYENTRANHTUAN).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new TruyenTranhTuanScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new TruyenTranhTuanScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.TRUYENTRANHTUAN, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.TRUYENTRANHTUAN).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new TruyenTranhTuanScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new TruyenTranhTuanScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.TRUYENTRANHTUAN, results);
        }
    }
}
