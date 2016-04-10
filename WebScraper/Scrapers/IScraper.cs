using System.Collections.Generic;
using WebScraper.Data;

namespace WebScraper.Scrapers
{
    interface IScraper
    {
        int GetTotalPages();
        List<Manga> GetMangaList(int pageIndex);
        List<Chapter> GetChapterList(string mangaUrl);
        List<Page> GetPageList(string chapterUrl);
    }
}
