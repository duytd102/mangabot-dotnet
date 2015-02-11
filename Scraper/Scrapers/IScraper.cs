using MangaDownloader.Data;
using Scraper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper
{
    public interface IScraper
    {
        int GetTotalPages();
        List<Manga> GetMangaList(int pageIndex);
        List<Chapter> GetChapterList(string mangaUrl);
        List<Page> GetPageList(string chapterUrl);

    }
}
