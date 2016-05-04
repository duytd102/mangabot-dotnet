using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Data;

namespace WebScraper.Processors
{
    public interface IProcessor
    {
        event Action<int, int, int, List<Manga>> ScrapOneMangaPageComplete;


        int GetTotalPages();
        List<Manga> GetMangaList();
        List<Chapter> GetChapterList(String mangaUrl);
        List<Page> GetPageList(String chapterUrl);
        void Cancel();
        bool IsCancel();
    }
}
