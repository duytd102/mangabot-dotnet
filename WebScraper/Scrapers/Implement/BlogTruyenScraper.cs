using Common;
using Common.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebScraper.Data;
using WebScraper.Enums;

namespace WebScraper.Scrapers.Implement
{
    class BlogTruyenScraper : IScraper
    {
        const string DOMAIN = "http://blogtruyen.com";
        const string ROOT_MANGALIST_URL = "http://blogtruyen.com/ListStory/GetListStory/";
        const string SCRIPT_URL = "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/scripts/blogtruyen.txt";
        const string CLASS_NAME = "WebScraper.Scrapers.Implement.BlogTruyenScraper";

        public int GetTotalPages()
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                int total = new BotCrawler<int>(SCRIPT_URL).Invoke(CLASS_NAME, "GetTotalPages");
                LogHelpers.Log("Total Pages: " + total);
                return total;
            }
            else
            {
                const int FIRST_PAGE = 1;
                int totalPages = 1;
                string pagingFilter = "<(?<TAG>\\w+)[^>]*?class\\s*=\\s*['|\"]\\s*page\\s*['|\"][^>]*?>.*?</\\k<TAG>>";
                string pagingIndexFilter = "<a[^>]*?href[^>]*?LoadPage\\s*\\((?<INDEX>\\d+)\\)[^>]*?>(?<TEXT>.*?)</a>";
                string pagingBlock = "";
                try
                {
                    String firstPageOfMangaListHtmlSrc = GetHtmlSrcOfMangaList(FIRST_PAGE);
                    MatchCollection mc = Regex.Matches(firstPageOfMangaListHtmlSrc, pagingFilter, RegexOptions.IgnoreCase);
                    foreach (Match m in mc)
                        pagingBlock += m.Value + Environment.NewLine;

                    mc = Regex.Matches(pagingBlock, pagingIndexFilter, RegexOptions.IgnoreCase);
                    totalPages = mc.Count > 0 ? int.Parse(mc[mc.Count - 1].Groups["INDEX"].Value) : 1;
                }
                catch { }
                return totalPages;
            }
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                results = new BotCrawler<List<Dictionary<String, String>>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetMangaList", new object[] { pageIndex });
            }
            else
            {
                string blockFilter = "<(?<TAG>\\w+)[^>]*?class\\s*=\\s*[\"|']\\s*tiptip[^>]*?[\"|'][^>]*?>(?<TEXT>.*?)</\\k<TAG>>";
                string nameAndUrlFilter = "<a[^>]*?href\\s*=\\s*[\"|'](?<MANGA_URL>.*?)[\"|'][^>]*?>(?<MANGA_NAME>.*?)</a>";
                string queryString = String.Format("Url=tatca&OrderBy=1&PageIndex={0}", pageIndex);
                try
                {
                    string mangaListHtmlSrc = HttpUtils.MakeHttpGet(ROOT_MANGALIST_URL, queryString);

                    MatchCollection blockes = Regex.Matches(mangaListHtmlSrc, blockFilter, RegexOptions.IgnoreCase);
                    foreach (Match blk in blockes)
                    {
                        Match nameAndUrlGroup = Regex.Match(blk.Groups["TEXT"].Value, nameAndUrlFilter, RegexOptions.IgnoreCase);
                        String name = Regex.Replace(nameAndUrlGroup.Groups["MANGA_NAME"].Value, "<img.*?>", "", RegexOptions.IgnoreCase).Trim();
                        
                        results.Add(new Dictionary<string, string>()
                        {
                            { "id", Guid.NewGuid().ToString() },
                            { "name", name },
                            { "url", nameAndUrlGroup.Groups["MANGA_URL"].Value }
                        });
                    }
                }
                catch { }
            }

            return DictionaryToList.ToMangaList(DOMAIN, MangaSite.BLOGTRUYEN, results);
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
                String mangaHtmlSrc = HttpUtils.MakeHttpGet(mangaUrl);
                HtmlDocument chapterDoc = new HtmlDocument();
                chapterDoc.LoadHtml(mangaHtmlSrc);

                HtmlNode listChapters = chapterDoc.GetElementbyId("list-chapters");
                if (listChapters != null)
                {
                    List<HtmlNode> chapterBlocks = listChapters.Descendants().Where(x => x.Name.Equals("p")).ToList();
                    foreach (HtmlNode chapterBlk in chapterBlocks)
                    {
                        try
                        {
                            HtmlNode title = chapterBlk.Descendants()
                                .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("title")).Descendants()
                                .FirstOrDefault(x => x.Name.Equals("a"));
                            HtmlNode publishedDate = chapterBlk.Descendants()
                                .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("publishedDate"));

                            string name = WebUtility.HtmlDecode(title.InnerText.Trim());
                            string url = title.GetAttributeValue("href", "").Replace("../", "").Trim();
                            if (url.IndexOf(DOMAIN) < 0)
                            {
                                url = HttpUtils.CombineUrl(DOMAIN, url);
                            }
                            url = WebUtility.HtmlDecode(url);

                            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(url))
                                continue;

                            results.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", name },
                                { "url", url }
                            });
                        }
                        catch (Exception) { }
                    }
                }
                else
                {
                    Uri uri = new Uri(mangaUrl);
                    String mobileMangaSrc = HttpUtils.MakeHttpGet(uri.Scheme + "://m." + uri.Host + uri.PathAndQuery);
                    HtmlDocument mobileDoc = new HtmlDocument();
                    mobileDoc.LoadHtml(mobileMangaSrc);

                    List<HtmlNode> rows = mobileDoc.DocumentNode.Descendants()
                        .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("danhsach")).Descendants()
                        .Where(x => x.GetAttributeValue("class", "").Contains("row")).ToList();

                    foreach (HtmlNode r in rows)
                    {
                        try
                        {
                            HtmlNode info = r.Descendants().First(x => x.Name.Equals("a"));
                            string name = WebUtility.HtmlDecode(info.InnerText.Trim());
                            string url = info.GetAttributeValue("href", "").Replace("../", "").Trim();
                            if (url.IndexOf(DOMAIN) < 0)
                            {
                                url = HttpUtils.CombineUrl(DOMAIN, url);
                            }
                            url = WebUtility.HtmlDecode(url);

                            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(url))
                                continue;

                            Chapter chapter = new Chapter();
                            chapter.ID = Guid.NewGuid().ToString();
                            chapter.Name = name;
                            chapter.Url = url;
                            chapter.Site = MangaSite.BLOGTRUYEN;
                            chapterList.Add(chapter);
                        }
                        catch { }
                    }
                }
            }

            return DictionaryToList.ToChapterList(DOMAIN, MangaSite.BLOGTRUYEN, results);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            if (CommonSettings.AppMode == AppMode.BETA || CommonSettings.AppMode == AppMode.PROD)
            {
                return new BotCrawler<List<Page>>(SCRIPT_URL).Invoke(CLASS_NAME, "GetPageList", new object[] { chapterUrl });
            }
            else
            {
                List<Page> pageList = new List<Page>();
                Page page;
                string listExp = "<(?<TAG>\\w+)[^>]*?id\\s*=\\s*[\"|']\\s*content\\s*[\"|'][^>]*?>(?<LIST>.*?)</\\k<TAG>>";
                string pageExp = "<img.*?src\\s*=\\s*[\"|'](?<URL>[^'|\"]*?)[\"|'].*?>";
                int index = 1;
                string url;
                try
                {
                    String chapterHtmlSrc = HttpUtils.MakeHttpGet(chapterUrl);
                    Match listBlock = Regex.Match(chapterHtmlSrc, listExp, RegexOptions.IgnoreCase);
                    MatchCollection pageBlockes = Regex.Matches(listBlock.Groups["LIST"].Value, pageExp, RegexOptions.IgnoreCase);
                    foreach (Match blk in pageBlockes)
                    {
                        url = blk.Groups["URL"].Value.Trim();
                        if (url.IndexOf("&url=", StringComparison.CurrentCulture) > -1)
                        {
                            Match m = Regex.Match(url, ".+&url=(?<ACTUAL_URL>[^&]+)");
                            url = m.Groups["ACTUAL_URL"].Value.Trim();
                        }
                        url = FixPhotoUrl(url);
                        url = WebUtility.HtmlDecode(url);

                        page = new Page();
                        page.ID = Guid.NewGuid().ToString();
                        page.Name = "Trang " + StringUtils.GenerateOrdinal(pageBlockes.Count, index);
                        page.Url = url;
                        page.LocalPath = "";
                        page.Site = MangaSite.BLOGTRUYEN;
                        pageList.Add(page);
                        index++;
                    }
                }
                catch { }
                return pageList;
            }
        }

        private string GetHtmlSrcOfMangaList(int pageIndex)
        {
            string queryString = String.Format("Url=tatca&OrderBy=1&PageIndex={0}", pageIndex);
            return HttpUtils.MakeHttpGet(ROOT_MANGALIST_URL, queryString);
        }

        private String FixPhotoUrl(String url)
        {
            String dest = url.Replace("2.bp.blogspot.com", "1.bp.blogspot.com")
                .Replace("3.bp.blogspot.com", "1.bp.blogspot.com")
                .Replace("4.bp.blogspot.com", "1.bp.blogspot.com")
                .Replace("?imgmax=6000", "")
                .Replace("?imgmax=3000", "")
                .Replace("?imgmax=2000", "")
                .Replace("?imgmax=1600", "")
                .Replace("?imgmax=0", "");

            bool isBlogspot = Regex.IsMatch(dest, "1.bp.blogspot.com");
            bool isImgur = Regex.IsMatch(dest, "i.imgur.com");

            if (isBlogspot)
            {
                String[] parts = dest.Split(new String[] { "1.bp.blogspot.com" }, StringSplitOptions.RemoveEmptyEntries);
                return parts.Length > 1 ? "http://1.bp.blogspot.com" + parts[1] + "?imgmax=0" : dest;
            }

            if (isImgur)
            {
                Regex re = new Regex("http:\\/\\/i.imgur.com\\/[A-Za-z0-9]{7}");
                String match = re.Match(dest).Value;
                return "http://images2-focus-opensocial.googleusercontent.com/gadgets/proxy?container=focus&gadget=a&no_expand=1&resize_h=0&rewriteMime=image/*&url=" + match + ".jpg";
            }

            return dest;
        }
    }
}
