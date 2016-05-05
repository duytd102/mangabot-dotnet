using Common;
using Common.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class OtakufcScraper : IScraper
    {
        private const string DOMAIN = "http://otakufc.com/";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/otakufc.cs";
        const string CLASS_NAME = "WebScraper.Scrapers.Implement.OtakufcScraper";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
            }
            else
            {
                return new OtakufcScript().GetTotalPages();
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
                results = new OtakufcScript().GetMangaList(pageIndex);
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.OTAKUFC, results);
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
                results = new OtakufcScript().GetChapterList(mangaUrl);
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.OTAKUFC, results);
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
                results = new OtakufcScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(MangaSite.OTAKUFC, results);
        }
    }
}
