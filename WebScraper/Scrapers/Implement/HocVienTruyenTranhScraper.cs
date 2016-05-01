using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class HocVienTruyenTranhScraper : IScraper
    {
        const string DOMAIN = "http://truyen.academyvn.com/";
        const string BASE_LIST_URL = "http://truyen.academyvn.com/manga/all?page=";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/hocvientruyentranh.cs";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.HocVienTruyenTranhScript";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
            }
            else
            {
                return new HocVienTruyenTranhScript().GetTotalPages();
            }
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                results = new BotCrawler<List<Dictionary<string, string>>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
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
                results = new BotCrawler<List<Dictionary<string, string>>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
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
                results = new BotCrawler<List<Dictionary<string, string>>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
            }
            else
            {
                results = new HocVienTruyenTranhScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.HOCVIENTRUYENTRANH, results);
        }
    }
}
