﻿using Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebScraper.Data;
using WebScraper.Enums;

namespace WebScraper.Scrapers.Implement
{
    class OtakufcScraper : IScraper
    {
        private const String BASE_LIST_URL = "http://otakufc.com/danhsach/key/all/";

        public int GetTotalPages()
        {
            string src = HttpUtils.MakeHttpGet(BASE_LIST_URL + 1);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);
            HtmlNode paginator = doc.DocumentNode.Descendants().FirstOrDefault(
                x => x.Name.Equals("ul") && x.GetAttributeValue("class", "").Contains("button-bar"));
            if (paginator != null)
            {
                HtmlNode lastPage = paginator.Descendants().Last(x => x.Name.Equals("li"));
                return int.Parse(lastPage.FirstChild.InnerText);
            }
            return 1;
        }

        public List<Manga> GetMangaList(int pageIndex)
        {
            List<Manga> mangaList = new List<Manga>();
            string listUrl = String.Format("{0}{1}", BASE_LIST_URL, pageIndex);
            string src = HttpUtils.MakeHttpGet(listUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            List<HtmlNode> blockContent = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("block_content")).ToList();
            foreach (HtmlNode bc in blockContent)
            {
                List<HtmlNode> trList = bc.Descendants().Where(x => x.Name.Equals("tr")).ToList();
                foreach (HtmlNode tr in trList)
                {
                    HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                    if (td != null)
                    {
                        HtmlNode a = td.FirstChild;

                        Manga manga = new Manga();
                        manga.ID = Guid.NewGuid().ToString();
                        manga.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                        manga.Url = WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim());
                        manga.Site = MangaSite.OTAKUFC;

                        if (String.IsNullOrEmpty(manga.Name) || String.IsNullOrEmpty(manga.Url))
                            continue;

                        mangaList.Add(manga);
                    }
                }
            }
            return mangaList;
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            List<Chapter> chapterList = new List<Chapter>();
            string src = HttpUtils.MakeHttpGet(mangaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(src);

            int slashIndex = mangaUrl.LastIndexOf("/");
            if (slashIndex == mangaUrl.Length - 1) slashIndex = mangaUrl.Substring(0, slashIndex).LastIndexOf("/");
            String lastPath = mangaUrl.Substring(slashIndex + 1);
            
            List<HtmlNode> blockContent = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "").Contains("block_content")).ToList();
            foreach (HtmlNode bc in blockContent)
            {
                List<HtmlNode> trList = bc.Descendants().Where(x => x.Name.Equals("tr")).ToList();
                foreach (HtmlNode tr in trList)
                {
                    HtmlNode td = tr.Descendants().FirstOrDefault(x => x.Name.Equals("td"));
                    if (td != null)
                    {
                        HtmlNode a = td.FirstChild;

                        Chapter chapter = new Chapter();
                        chapter.ID = Guid.NewGuid().ToString();
                        chapter.Name = WebUtility.HtmlDecode(a.InnerText.Trim());
                        chapter.Url = mangaUrl + WebUtility.HtmlDecode(a.GetAttributeValue("href", "").Trim()).Replace(lastPath, "");
                        chapter.Site = MangaSite.OTAKUFC;

                        if (String.IsNullOrEmpty(chapter.Name) || String.IsNullOrEmpty(chapter.Url))
                            continue;

                        chapterList.Add(chapter);
                    }
                }
            }
            return chapterList;
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            const string pattern = "lstImages.push\\([\"|'](?<URL>.+?)[\"|']\\)";
            List<Page> pageList = new List<Page>();
            int index = 1;

            string src = HttpUtils.MakeHttpGet(chapterUrl);
            MatchCollection list = Regex.Matches(src, pattern, RegexOptions.IgnoreCase);
            foreach (Match img in list)
            {
                string url = img.Groups["URL"].Value.Trim();
                Page page = new Page();
                page.ID = Guid.NewGuid().ToString();
                page.Name = "Trang " + StringUtils.GenerateOrdinal(list.Count, index);
                page.Url = WebUtility.HtmlDecode(url);
                page.Site = MangaSite.OTAKUFC;

                if (String.IsNullOrEmpty(page.Url))
                    continue;

                pageList.Add(page);
                index++;
            }
            return pageList;
        }
    }
}