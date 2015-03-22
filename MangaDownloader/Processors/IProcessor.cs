using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Data;

namespace MangaDownloader.Processors
{
    public interface IProcessor
    {
        event Action<int, int, int, List<Manga>> ScrapOneMangaPageComplete;
        int GetTotalPages();
        List<Manga> GetMangaList();
        List<Chapter> GetChapterList(String mangaUrl);
        List<Page> GetPageList(String chapterUrl);
    }
}
