using System;
using System.Collections.Generic;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Scrapers;

namespace MangaDownloader.Processors.Implement
{
    public class MultiplePagesProcessor : IProcessor
    {
        public event Action<int, int, int, List<Manga>> ScrapOneMangaPageComplete;

        int totalPages;
        int limitRows;
        MangaSite currentSite;
        IScraper scraper;

        public MultiplePagesProcessor(MangaSite site)
        {
            currentSite = site;
            scraper = ScraperFactory.CreateScraper(site);
        }

        public int GetTotalPages()
        {
            totalPages = scraper.GetTotalPages();
            return totalPages;
        }

        public List<Manga> GetMangaList()
        {
            List<Manga> mangaList = new List<Manga>();
            List<Manga> partialList;
            int totalManga = 0;
            totalPages = GetTotalPages();

            for (int i = 1; i <= totalPages; i++)
            {
                partialList = scraper.GetMangaList(i);
                mangaList.AddRange(partialList);

                if (i == 1)
                {
                    limitRows = partialList.Count;
                    totalManga = limitRows * totalPages;
                }
                else if (i < totalPages && limitRows != partialList.Count)
                    limitRows = 200;

                ScrapOneMangaPageComplete(totalManga, totalPages, i, partialList);
            }

            return mangaList;
        }

        public List<Chapter> GetChapterList(string mangaUrl)
        {
            return scraper.GetChapterList(mangaUrl);
        }

        public List<Page> GetPageList(string chapterUrl)
        {
            return scraper.GetPageList(chapterUrl);
        }
    }
}
