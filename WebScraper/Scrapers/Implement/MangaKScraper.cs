﻿using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Scrapers.Scripts;

namespace WebScraper.Scrapers.Implement
{
    class MangaKScraper : IScraper
    {
        const string DOMAIN = "http://mangak.info";
        const string CLASS_NAME = "WebScraper.Scrapers.Scripts.MangaKScript";
        const MangaSite SITE = MangaSite.MANGAK;

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
            return new MangaKScript().GetTotalPages();
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
                    results = new MangaKScript().GetMangaList(pageIndex);
                }
            }
            else
            {
                results = new MangaKScript().GetMangaList(pageIndex);
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
                    results = new MangaKScript().GetChapterList(mangaUrl);
                }
            }
            else
            {
                results = new MangaKScript().GetChapterList(mangaUrl);
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
                    results = new MangaKScript().GetPageList(chapterUrl);
                }
            }
            else
            {
                results = new MangaKScript().GetPageList(chapterUrl);
            }

            return DictionaryToList.ToPageList(SITE, results);
        }
    }
}
