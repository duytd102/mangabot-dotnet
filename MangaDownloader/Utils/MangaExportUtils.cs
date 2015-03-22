using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Data;
using WebScraper.Enums;

namespace MangaDownloader.Utils
{
    class MangaExportUtils
    {
        public static void Export(MangaSite site, List<Manga> mangaList)
        {
            try
            {
                String mangaFilePath = String.Format("{0}\\{1}.csv", Application.StartupPath, site.ToString().ToLower());
                using (StreamWriter sw = new StreamWriter(mangaFilePath, false, Encoding.UTF8))
                {
                    var csv = new CsvWriter(sw);
                    csv.WriteField("Name");
                    csv.WriteField("Url");
                    csv.NextRecord();

                    foreach (Manga manga in mangaList)
                    {
                        csv.WriteField(manga.Name);
                        csv.WriteField(manga.Url);
                        csv.NextRecord();
                    }

                    sw.Close();
                }
            }
            catch { }
        }

        public static List<Manga> Import(MangaSite site)
        {
            List<Manga> mangaList = new List<Manga>();
            Manga manga;
            String mangaFilePath = String.Format("{0}\\{1}.csv", Application.StartupPath, site.ToString().ToLower());
            try
            {
                using (StreamReader sr = new StreamReader(mangaFilePath, Encoding.UTF8))
                {
                    var csv = new CsvReader(sr);
                    while (csv.Read())
                    {
                        manga = new Manga();
                        manga.ID = Guid.NewGuid().ToString();
                        manga.Name = csv.GetField<string>(0);
                        manga.Url = csv.GetField<string>(1);
                        manga.Site = site;
                        mangaList.Add(manga);
                    }

                    sr.Close();
                }
            }
            catch { }
            return mangaList;
        }
    }
}
