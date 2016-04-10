using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Utils;

namespace WebScraper.Scrapers.Implement
{
    class VeChaiScraper : IScraper
    {
        private const String DOMAIN = "http://vechai.info/";
        private const String ROOT_LIST = "http://vechai.info/danh-sach.tall.html?p=";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/vechai.txt";
        const string CLASS_NAME = "WebScraper.Scrapers.Implement.VeChaiScraper";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
            }
            else
            {
                string listUrl = ROOT_LIST + 1;
                string src = HttpUtils.MakeHttpGet(listUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                HtmlNode pagination = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("pagination"));
                HtmlNode lastA = pagination.Descendants().LastOrDefault(x => x.Name.Equals("a"));
                string index = lastA.GetAttributeValue("href", "").Replace(ROOT_LIST, "");
                return int.Parse(index);
            }
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<List<Manga>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
            }
            else
            {
                List<Manga> mangaList = new List<Manga>();
                string listUrl = ROOT_LIST + pageIndex;
                string src = HttpUtils.MakeHttpGet(listUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                List<HtmlNode> tables = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("table")).ToList();
                foreach (HtmlNode table in tables)
                {
                    List<HtmlNode> trList = table.Descendants().Where(x => x.Name.Equals("tr")).ToList();
                    foreach (HtmlNode tr in trList)
                    {
                        List<HtmlNode> tdList = tr.Elements("td").ToList();
                        foreach (HtmlNode td in tdList)
                        {
                            HtmlNode a = td.Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                            if (a != null)
                            {
                                Manga manga = new Manga();
                                manga.ID = Guid.NewGuid().ToString();
                                manga.Name = WebUtility.HtmlDecode(GetInnerTextWithoutElements(a));
                                manga.Url = WebUtility.HtmlDecode(UrlUtils.FixUrl(DOMAIN, a.GetAttributeValue("href", "").Trim()));
                                manga.Site = MangaSite.VECHAI;

                                if (String.IsNullOrEmpty(manga.Name) || String.IsNullOrEmpty(manga.Url))
                                    continue;

                                mangaList.Add(manga);
                            }
                        }
                    }
                }

                return mangaList;
            }
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<List<Chapter>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetChapterList", new object[] { mangaUrl });
            }
            else
            {
                List<Chapter> chapterList = new List<Chapter>();
                string src = HttpUtils.MakeHttpGet(mangaUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                List<HtmlNode> chapterContainerList = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("total-chapter")).ToList();
                foreach (HtmlNode chapterContainer in chapterContainerList)
                {
                    if (chapterContainer.Descendants().Count(x => x.GetAttributeValue("class", "").Contains("content")) > 0)
                    {
                        List<HtmlNode> aList = chapterContainer.Descendants().Where(x => x.Name.Equals("a")).ToList();
                        foreach (HtmlNode a in aList)
                        {
                            Chapter chapter = new Chapter();
                            chapter.ID = Guid.NewGuid().ToString();
                            chapter.Name = WebUtility.HtmlDecode(GetInnerTextWithoutElements(a));
                            chapter.Url = WebUtility.HtmlDecode(UrlUtils.FixUrl(DOMAIN, a.GetAttributeValue("href", "")));
                            chapter.Site = MangaSite.VECHAI;

                            if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                                continue;

                            chapterList.Add(chapter);
                        }
                    }
                }

                return chapterList;
            }
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            if (CommonSettings.AppMode == CommonSettings.Mode.BETA || CommonSettings.AppMode == CommonSettings.Mode.PROD)
            {
                return new BotCrawler<List<Page>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
            }
            else
            {
                List<Page> pageList = new List<Page>();
                int index = 1;

                string src = HttpUtils.MakeHttpGet(chapterUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(src);

                HtmlNode container = doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("each-page"));
                List<HtmlNode> imgTags = container.Descendants().Where(x => x.Name.Equals("img")).ToList();
                foreach (HtmlNode img in imgTags)
                {
                    Page page = new Page();
                    page.ID = Guid.NewGuid().ToString();
                    page.Name = "Trang " + StringUtils.GenerateOrdinal(imgTags.Count, index);
                    page.Url = WebUtility.HtmlDecode(img.GetAttributeValue("src", "").Trim());
                    page.Site = MangaSite.VECHAI;

                    if (String.IsNullOrEmpty(page.Url))
                        continue;

                    pageList.Add(page);
                    index++;
                }
                return pageList;
            }
        }

        private string GetInnerTextWithoutElements(HtmlNode a)
        {
            string name = "";
            foreach (HtmlNode txtNode in a.Elements("#text"))
            {
                if (String.IsNullOrWhiteSpace(txtNode.InnerText) == false)
                {
                    name += (txtNode.InnerText.Trim() + " ");
                    break;
                }
            }
            return name.Trim();
        }
    }
}