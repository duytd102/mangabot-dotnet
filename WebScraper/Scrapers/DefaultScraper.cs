using Common;
using Common.Enums;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Utils;

namespace WebScraper.Scrapers
{
    class DefaultScraper : IScraper
    {
        private MangaSite site;
        private string scriptClassName;
        private string domain;

        public DefaultScraper(MangaSite site, string scriptClassName, string domain)
        {
            this.site = site;
            this.scriptClassName = scriptClassName;
            this.domain = domain;
        }

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    return new BotCrawler<int>(site).Invoke(scriptClassName, "GetTotalPages");
                }
                catch { }
            }
            
            return new MethodInvoker<int>().Invoke(scriptClassName, "GetTotalPages", null);
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(site).Invoke(scriptClassName, "GetMangaList", new object[] { pageIndex });
                }
                catch { }
            }

            if (results.Count == 0)
            {
                results = new MethodInvoker<List<Dictionary<string, string>>>().Invoke(scriptClassName, "GetMangaList", new object[] { pageIndex });
            }

            return DictionaryToList.ToMangaList(domain, site, results);
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(site).Invoke(scriptClassName, "GetChapterList", new object[] { mangaUrl });
                }
                catch { }
            }

            if (results.Count == 0)
            {
                results = new MethodInvoker<List<Dictionary<string, string>>>().Invoke(scriptClassName, "GetChapterList", new object[] { mangaUrl });
            }

            return DictionaryToList.ToChapterList(domain, site, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                try
                {
                    results = new BotCrawler<List<Dictionary<string, string>>>(site).Invoke(scriptClassName, "GetPageList", new object[] { chapterUrl });
                }
                catch { }
            }

            if (results.Count == 0)
            {
                results = new MethodInvoker<List<Dictionary<string, string>>>().Invoke(scriptClassName, "GetPageList", new object[] { chapterUrl });
            }

            return DictionaryToList.ToPageList(site, results);
        }

    }
}
