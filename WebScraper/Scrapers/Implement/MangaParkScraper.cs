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
    class MangaParkScraper : IScraper
    {
        const string BASE_LIST_URL = "http://mangapark.me/latest/";
        const string DOMAIN = "http://mangapark.me/";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/mangapark.txt";
        const string CLASS_NAME = "WebScraper.Scrapers.Implement.MangaParkScraper";

        int IScraper.GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
            }
            else
            {
                string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);
                HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("paging-bar"));
                if (paginator != null)
                {
                    int max = 1;
                    List<HtmlNode> aEleList = paginator.Descendants().Where(x => x.Name.Equals("a")).ToList();
                    foreach (HtmlNode aEle in aEleList)
                    {
                        string href = aEle.GetAttributeValue("href", "");
                        int idx = 0;
                        if (int.TryParse(Regex.Replace(href, ".+/", ""), out idx))
                        {
                            if (max < idx)
                            {
                                max = idx;
                            }
                        }
                    }

                    return max;
                }
                return 1;
            }
        }

        List<Manga> IScraper.GetMangaList(int pageIndex)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Manga>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
            }
            else
            {
                string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + pageIndex);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);
                List<Manga> mangaList = new List<Manga>();
                List<HtmlNode> items = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("item")).ToList();
                foreach (HtmlNode item in items)
                {
                    HtmlNode a = item.Descendants().FirstOrDefault(x => x.Name.Equals("ul")).Element("h3").Element("a");

                    Manga manga = new Manga();
                    manga.ID = Guid.NewGuid().ToString();
                    manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                    manga.Url = WebUtility.HtmlDecode(UrlUtils.FixUrl(DOMAIN, a.GetAttributeValue("href", "").Trim()));
                    manga.Site = MangaSite.MANGAPARK;

                    if (String.IsNullOrEmpty(manga.Name) || String.IsNullOrEmpty(manga.Url))
                        continue;

                    mangaList.Add(manga);
                }
                return mangaList;
            }
        }

        List<Chapter> IScraper.GetChapterList(string mangaUrl)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Chapter>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
            }
            else
            {
                string src = HttpUtils.MakeHttpGet(mangaUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                List<Chapter> chapterList = new List<Chapter>();

                List<HtmlNode> streams = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("stream")).ToList();
                foreach (HtmlNode stream in streams)
                {
                    HtmlNode h3 = stream.Element("h3");
                    string version = "";
                    if (h3 != null)
                    {
                        HtmlNode versionNode = h3.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("st"));
                        if (versionNode != null)
                        {
                            version = versionNode.InnerText.Trim() + " - ";
                        }
                    }

                    HtmlNode volume = stream.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("volume"));
                    List<HtmlNode> chapters = volume.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("chapter")).Elements("li").ToList();
                    foreach (HtmlNode chapterNode in chapters)
                    {
                        HtmlNode chapterName = chapterNode.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                        HtmlNode a = chapterNode.Descendants().LastOrDefault(x => x.Name.Equals("a"));

                        Chapter chapter = new Chapter();
                        chapter.ID = Guid.NewGuid().ToString();
                        chapter.Name = WebUtility.HtmlDecode(version + chapterName.InnerText.Trim());
                        chapter.Url = WebUtility.HtmlDecode(UrlUtils.FixUrl(DOMAIN, a.GetAttributeValue("href", "").Trim()));
                        chapter.Site = MangaSite.MANGAPARK;

                        if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                            continue;

                        chapterList.Add(chapter);
                    }
                }

                return chapterList;
            }
        }

        List<Page> IScraper.GetPageList(string chapterUrl)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Page>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
            }
            else
            {
                string src = HttpUtils.MakeHttpGet(chapterUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                List<Page> pageList = new List<Page>();
                int index = 1;

                List<HtmlNode> canvasList = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("canvas")).ToList();
                foreach (HtmlNode canvas in canvasList)
                {
                    HtmlNode img = canvas.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("img-link")).Element("img");

                    Page page = new Page();
                    page.ID = Guid.NewGuid().ToString();
                    page.Name = "Trang " + StringUtils.GenerateOrdinal(canvasList.Count, index);
                    page.Url = WebUtility.HtmlDecode(img.GetAttributeValue("src", "").Trim());
                    page.Site = MangaSite.MANGAPARK;

                    if (String.IsNullOrEmpty(page.Url) == false)
                    {
                        pageList.Add(page);
                        index++;
                    }
                }
                return pageList;
            }
        }
    }
}
