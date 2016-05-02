using Common;
using Common.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebScraper.Data;

namespace WebScraper.Scrapers.Implement
{
    class TruyenTranhTuanScraper : IScraper
    {
        private const String LIST_URL = "http://truyentranhtuan.com/danh-sach-truyen";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/truyentranhtuan.txt";
        const string CLASS_NAME = "WebScraper.Scrapers.Implement.TruyenTranhTuanScraper";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
            }
            else
            {
                return 1;
            }
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Manga>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
            }
            else
            {
                List<Manga> mangaList = new List<Manga>();
                string src = HttpUtils.MakeHttpGet(LIST_URL);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                HtmlNode container = doc.GetElementbyId("new-chapter");
                List<HtmlNode> list = container.Descendants().Where(
                    x => x.GetAttributeValue("class", "").Contains("manga-focus")).ToList();

                foreach (HtmlNode m in list)
                {
                    HtmlNode a = m.Descendants().First(x => x.GetAttributeValue("class", "").Contains("manga")).FirstChild;

                    Manga manga = new Manga();
                    manga.ID = Guid.NewGuid().ToString();
                    manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    manga.Site = MangaSite.TRUYENTRANHTUAN;

                    if (String.IsNullOrEmpty(manga.Name) || String.IsNullOrEmpty(manga.Url))
                        continue;

                    mangaList.Add(manga);
                }

                return mangaList;
            }
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Chapter>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
            }
            else
            {
                List<Chapter> chapterList = new List<Chapter>();
                string src = HttpUtils.MakeHttpGet(mangaUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                HtmlNode container = doc.GetElementbyId("manga-chapter");
                List<HtmlNode> list = container.Descendants().Where(
                    x => x.GetAttributeValue("class", "").Contains("chapter-name")).ToList();

                foreach (HtmlNode c in list)
                {
                    HtmlNode a = c.FirstChild;

                    Chapter chapter = new Chapter();
                    chapter.ID = Guid.NewGuid().ToString();
                    chapter.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    chapter.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                    chapter.Site = MangaSite.TRUYENTRANHTUAN;

                    if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                        continue;

                    chapterList.Add(chapter);
                }
                return chapterList;
            }
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Page>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
            }
            else
            {
                const string pattern = @"slides_page_url_path\s*=\s*\[\s*(?<urls>[^\]]+)\]";
                List<Page> pageList = new List<Page>();
                int index = 1;
                string src = HttpUtils.MakeHttpGet(chapterUrl);

                Match arr = Regex.Match(src, pattern);
                string grp = arr.Groups["urls"].Value;
                string[] list = grp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string u in list)
                {
                    Page page = new Page();
                    page.ID = Guid.NewGuid().ToString();
                    page.Name = "Trang " + StringUtils.GenerateOrdinal(list.Length, index);
                    page.Url = WebUtility.HtmlDecode(u.Replace("\"", "").Replace("'", "").Trim());
                    page.Site = MangaSite.TRUYENTRANHTUAN;

                    if (String.IsNullOrEmpty(page.Url))
                        continue;

                    pageList.Add(page);
                    index++;
                }
                return pageList;
            }
        }
    }
}
