using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class HocVienTruyenTranhScraper : IScraper
    {
        const string DOMAIN = "http://truyen.academyvn.com/";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.HocVienTruyenTranhScript";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(MangaSite.HOCVIENTRUYENTRANH).Invoke(CLASS_NAME, "GetTotalPages");
                }
                catch { }
            }
            return new HocVienTruyenTranhScript().GetTotalPages();
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.HOCVIENTRUYENTRANH).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
                }
                catch
                {
                    results = new HocVienTruyenTranhScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new HocVienTruyenTranhScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.HOCVIENTRUYENTRANH, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.HOCVIENTRUYENTRANH).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
                }
                catch
                {
                    results = new HocVienTruyenTranhScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new HocVienTruyenTranhScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.HOCVIENTRUYENTRANH, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(MangaSite.HOCVIENTRUYENTRANH).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
                }
                catch
                {
                    results = new HocVienTruyenTranhScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new HocVienTruyenTranhScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.HOCVIENTRUYENTRANH, results);
        }
    }
}
