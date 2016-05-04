using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class BlogTruyenScript
    {
        const string ROOT_MANGALIST_URL = "http://blogtruyen.com/ListStory/GetListStory/";

        public int GetTotalPages()
        {
            int totalPages = 1;
            try
            {
                string pagingFilter = "<(?<TAG>\\w+)[^>]*?class\\s*=\\s*['|\"]\\s*page\\s*['|\"][^>]*?>.*?</\\k<TAG>>";
                string pagingIndexFilter = "<a[^>]*?href[^>]*?LoadPage\\s*\\((?<INDEX>\\d+)\\)[^>]*?>(?<TEXT>.*?)</a>";
                string pagingBlock = "";
                string queryString = string.Format("Url=tatca&OrderBy=1&PageIndex={0}", 1);
                string firstPageOfMangaListHtmlSrc = HttpUtils.MakeHttpGet(ROOT_MANGALIST_URL, queryString);
                MatchCollection mc = Regex.Matches(firstPageOfMangaListHtmlSrc, pagingFilter, RegexOptions.IgnoreCase);
                foreach (Match m in mc)
                {
                    pagingBlock += m.Value + Environment.NewLine;
                }

                mc = Regex.Matches(pagingBlock, pagingIndexFilter, RegexOptions.IgnoreCase);
                totalPages = mc.Count > 0 ? int.Parse(mc[mc.Count - 1].Groups["INDEX"].Value) : 1;
            }
            catch { }
            return totalPages;
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            try
            {
                string blockFilter = "<(?<TAG>\\w+)[^>]*?class\\s*=\\s*[\"|']\\s*tiptip[^>]*?[\"|'][^>]*?>(?<TEXT>.*?)</\\k<TAG>>";
                string nameAndUrlFilter = "<a[^>]*?href\\s*=\\s*[\"|'](?<MANGA_URL>.*?)[\"|'][^>]*?>(?<MANGA_NAME>.*?)</a>";
                string queryString = string.Format("Url=tatca&OrderBy=1&PageIndex={0}", pageIndex);

                string mangaListHtmlSrc = HttpUtils.MakeHttpGet(ROOT_MANGALIST_URL, queryString);

                MatchCollection blockes = Regex.Matches(mangaListHtmlSrc, blockFilter, RegexOptions.IgnoreCase);
                foreach (Match blk in blockes)
                {
                    Match nameAndUrlGroup = Regex.Match(blk.Groups["TEXT"].Value, nameAndUrlFilter, RegexOptions.IgnoreCase);
                    string name = Regex.Replace(nameAndUrlGroup.Groups["MANGA_NAME"].Value, "<img.*?>", "", RegexOptions.IgnoreCase).Trim();
                    string url = nameAndUrlGroup.Groups["MANGA_URL"].Value;

                    if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                    {
                        results.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", name },
                                { "url", url }
                            });
                    }
                }
            }
            catch { }

            return results;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            string mangaHtmlSrc = HttpUtils.MakeHttpGet(mangaUrl);
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
                        HtmlNode title = chapterBlk.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("title")).Descendants().FirstOrDefault(x => x.Name.Equals("a"));
                        string name = title.InnerText.Trim();
                        string url = title.GetAttributeValue("href", "").Replace("../", "").Trim();

                        if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                        {
                            results.Add(new Dictionary<string, string>()
                                {
                                    { "id", Guid.NewGuid().ToString() },
                                    { "name", name },
                                    { "url", url }
                                });
                        }
                    }
                    catch { }
                }
            }
            else
            {
                Uri uri = new Uri(mangaUrl);
                string mobileMangaSrc = HttpUtils.MakeHttpGet(uri.Scheme + "://m." + uri.Host + uri.PathAndQuery);
                HtmlDocument mobileDoc = new HtmlDocument();
                mobileDoc.LoadHtml(mobileMangaSrc);

                List<HtmlNode> rows = mobileDoc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("danhsach")).Descendants().Where(x => x.GetAttributeValue("class", "").Contains("row")).ToList();
                foreach (HtmlNode r in rows)
                {
                    try
                    {
                        HtmlNode info = r.Descendants().First(x => x.Name.Equals("a"));
                        string name = info.InnerText.Trim();
                        string url = info.GetAttributeValue("href", "").Replace("../", "").Trim();

                        if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                        {
                            results.Add(new Dictionary<string, string>()
                                {
                                    { "id", Guid.NewGuid().ToString() },
                                    { "name", name },
                                    { "url", url }
                                });
                        }
                    }
                    catch { }
                }
            }

            return results;
        }

        public List<Dictionary<string, string>> GetPageList(string chapterUrl)
        {
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            try
            {
                string listExp = "<(?<TAG>\\w+)[^>]*?id\\s*=\\s*[\"|']\\s*content\\s*[\"|'][^>]*?>(?<LIST>.*?)</\\k<TAG>>";
                string pageExp = "<img.*?src\\s*=\\s*[\"|'](?<URL>[^'|\"]*?)[\"|'].*?>";
                int index = 1;

                string chapterHtmlSrc = HttpUtils.MakeHttpGet(chapterUrl);
                Match listBlock = Regex.Match(chapterHtmlSrc, listExp, RegexOptions.IgnoreCase);
                MatchCollection pageBlockes = Regex.Matches(listBlock.Groups["LIST"].Value, pageExp, RegexOptions.IgnoreCase);
                foreach (Match blk in pageBlockes)
                {
                    string url = blk.Groups["URL"].Value.Trim();
                    if (url.IndexOf("&url=", StringComparison.CurrentCulture) > -1)
                    {
                        Match m = Regex.Match(url, ".+&url=(?<ACTUAL_URL>[^&]+)");
                        url = m.Groups["ACTUAL_URL"].Value.Trim();
                    }
                    url = FixPhotoUrl(url);

                    if (string.IsNullOrWhiteSpace(url) == false)
                    {
                        results.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", "Trang " + StringUtils.GenerateOrdinal(pageBlockes.Count, index) },
                                { "url", url }
                            });
                    }

                    index++;
                }
            }
            catch { }

            return results;
        }

        private string FixPhotoUrl(string url)
        {
            string dest = url.Replace("2.bp.blogspot.com", "1.bp.blogspot.com")
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
                string[] parts = dest.Split(new string[] { "1.bp.blogspot.com" }, StringSplitOptions.RemoveEmptyEntries);
                return parts.Length > 1 ? "http://1.bp.blogspot.com" + parts[1] + "?imgmax=0" : dest;
            }

            if (isImgur)
            {
                Regex re = new Regex("http:\\/\\/i.imgur.com\\/[A-Za-z0-9]{7}");
                string match = re.Match(dest).Value;
                return "http://images2-focus-opensocial.googleusercontent.com/gadgets/proxy?container=focus&gadget=a&no_expand=1&resize_h=0&rewriteMime=image/*&url=" + match + ".jpg";
            }

            return dest;
        }
    }
}
