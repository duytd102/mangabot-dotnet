using System;
using System.Collections.Generic;
using WebScraper.Data;

namespace WebScraper.Scrapers
{
    class CloudflareScraper : IScraper
    {
        int IScraper.GetTotalPages()
        {
            throw new NotImplementedException();
        }

        List<Manga> IScraper.GetMangaList(int pageIndex)
        {
            throw new NotImplementedException();
        }

        List<Chapter> IScraper.GetChapterList(string mangaUrl)
        {
            throw new NotImplementedException();
        }

        List<Page> IScraper.GetPageList(string chapterUrl)
        {
            throw new NotImplementedException();
        }
    }
}
