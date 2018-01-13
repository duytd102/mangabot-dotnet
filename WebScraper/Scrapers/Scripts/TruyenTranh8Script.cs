using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper.Scrapers.Scripts
{
    public class TruyenTranh8Script
    {
        const string BASE_LIST_URL = "http://truyentranh8.net/danh_sach_truyen/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode tblChap = doc.GetElementbyId("tblChap");
            HtmlNode lastA = tblChap.Descendants().LastOrDefault(x => x.Name.Equals("a") && x.GetAttributeValue("data-page", null) != null);

            return lastA == null ? 1 : lastA.GetAttributeValue("data-page", 1);
        }

        public List<Dictionary<string, string>> GetMangaList(int pageIndex)
        {
            List<Dictionary<string, string>> mangaList = new List<Dictionary<string, string>>();

            string listUrl = pageIndex > 1 ? String.Format("{0}page={1}", BASE_LIST_URL, pageIndex) : BASE_LIST_URL;
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode tblChap = doc.GetElementbyId("tblChap");
            List<HtmlNode> trTags = tblChap.Descendants().Where(x => x.Name.Equals("tr")).ToList();
            foreach (HtmlNode tr in trTags)
            {
                HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td") && x.GetAttributeValue("class", "").Contains("tit"));

                if (td != null)
                {
                    HtmlNode a = td.Element("a");
                    string name = a.InnerText.Trim();
                    string url = a.GetAttributeValue("href", "");

                    if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                    {
                        mangaList.Add(new Dictionary<string, string>()
                            {
                                { "id", Guid.NewGuid().ToString() },
                                { "name", name },
                                { "url", url }
                            });
                    }
                }
            }

            return mangaList;
        }

        public List<Dictionary<string, string>> GetChapterList(string mangaUrl)
        {
            List<Dictionary<string, string>> chapterList = new List<Dictionary<string, string>>();

            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            HtmlNode chapListElement = doc.GetElementbyId("ChapList");
            List<HtmlNode> aTags = chapListElement.Descendants().Where(x => x.GetAttributeValue("itemprop", "").Contains("itemListElement")).ToList();
            foreach (HtmlNode a in aTags)
            {
                string url = a.GetAttributeValue("href", "");

                HtmlNode title = a.Descendants().FirstOrDefault(x => x.Name.Equals("h2"));
                string name = title == null ? "" : title.InnerText.Trim();

                if (string.IsNullOrWhiteSpace(name) == false && string.IsNullOrWhiteSpace(url) == false)
                {
                    chapterList.Add(new Dictionary<string, string>()
                    {
                        { "id", Guid.NewGuid().ToString() },
                        { "name", name },
                        { "url", url }
                    });
                }
            }

            return chapterList;
        }

        public List<Dictionary<string, string>> GetPageList(string chapterUrl)
        {
            string src = HttpUtils.MakeHttpGet(chapterUrl);

            int index = 1;
            List<Dictionary<string, string>> pageList = new List<Dictionary<string, string>>();

            const string scriptPattern = "<script[^>]*?>(?<TEXT>.*?)</script>";
            MatchCollection scripts = Regex.Matches(src, scriptPattern, RegexOptions.IgnoreCase);
            foreach (Match script in scripts)
            {
                string sc = script.Groups["TEXT"].Value;
                const string fn = @"eval\(.+?}\((?<TEXT>.+?)\)\)";
                Match fnm = Regex.Match(sc, fn);
                if (fnm.Success)
                {
                    sc = fnm.Groups["TEXT"].Value;

                    const string cover = "(?<COVER>['\"])(?<TEXT>.+?)\\k<COVER>";
                    string p; int a; int c; string[] k; int e; Dictionary<string, string> d;

                    string[] parts = sc.Split(',');
                    if (parts.Length >= 5)
                    {
                        p = Regex.Match(parts[0], cover).Groups["TEXT"].Value;
                        a = int.Parse(parts[1]);
                        c = int.Parse(parts[2]);
                        k = Regex.Match(parts[3], cover).Groups["TEXT"].Value.Split('|');
                        e = int.Parse(parts[4]);
                        d = new Dictionary<string, string>();

                        string deobfusCode = Deobfuscating(p, a, c, k, e, d);

                        const string lstImagesPattern = "lstImages\\[(?<INDEX>\\d+)\\]=\"(?<URL>.+?)\"";
                        const string lstImagesVIPPattern = "lstImagesVIP\\[(?<INDEX>\\d+)\\]=\"(?<URL>.+?)\"";

                        MatchCollection imgList = Regex.Matches(deobfusCode, lstImagesPattern);
                        MatchCollection vipList = Regex.Matches(deobfusCode, lstImagesVIPPattern);

                        Dictionary<int, string> imgDic = new Dictionary<int, string>();
                        Dictionary<int, string> vipDic = new Dictionary<int, string>();

                        foreach (Match im in imgList)
                        {
                            imgDic.Add(int.Parse(im.Groups["INDEX"].Value), im.Groups["URL"].Value);
                        }

                        foreach (Match im in vipList)
                        {
                            vipDic.Add(int.Parse(im.Groups["INDEX"].Value), im.Groups["URL"].Value);
                        }

                        List<int> imgIndex = SortKeys(imgDic);

                        foreach (var i in imgIndex)
                        {
                            string url = imgDic[i].Trim();

                            if (vipDic.ContainsKey(i) && !string.IsNullOrWhiteSpace(vipDic[i]))
                            {
                                url = vipDic[i].Trim();
                            }

                            if (!string.IsNullOrWhiteSpace(url))
                            {
                                pageList.Add(new Dictionary<string, string>()
                                    {
                                        { "id", Guid.NewGuid().ToString() },
                                        { "name", "Trang " + StringUtils.GenerateOrdinal(imgIndex.Count, index) },
                                        { "url", url }
                                    });

                                index++;
                            }
                        }
                    }
                }
            }

            return pageList;
        }

        private List<int> SortKeys(Dictionary<int, string> dic)
        {
            List<int> list = dic.Keys.ToList();
            list.Sort();
            return list;
        }

        private static string Deobfuscating(string p, int a, int c, string[] k, int e, Dictionary<string, string> d)
        {
            while (--c >= 0)
            {
                if (c < k.Length && !string.IsNullOrEmpty(k[c]))
                {
                    d[fn2(c, a)] = k[c];
                }
                else
                {
                    d[fn2(c, a)] = fn2(c, a);
                }
            }
            c = 1;
            return new Regex(@"\b\w+\b").Replace(p, match => d[match.Value]);
        }

        private static string fn2(int c, int a)
        {
            return (c < a ? "" : fn2(c / a, a)) + ((c = c % a) > 35 ? Convert.ToChar(c + 29).ToString() : toRadix(c, 36));
        }

        private static string toRadix(decimal N, int radix)
        {
            string HexN = "";
            decimal Q = Math.Floor(Math.Abs(N));
            int R;
            while (true)
            {
                R = (int)Q % radix;
                HexN = "0123456789abcdefghijklmnopqrstuvwxyz"[R] + HexN;
                Q = (Q - R) / radix;
                if (Q == 0) break;
            }
            return ((N < 0) ? "-" + HexN : HexN);
        }
    }
}
