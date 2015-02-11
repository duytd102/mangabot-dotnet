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
                String mangaFilePath = String.Format("{0}\\{1}.csv", Application.StartupPath, site);
                using (StreamWriter sw = new StreamWriter(mangaFilePath, false, Encoding.UTF8))
                {
                    foreach (Manga manga in mangaList)
                        sw.WriteLine(String.Format("\"{0}\",\"{1}\"", manga.Name, manga.Url));
                    sw.Close();
                }
            }
            catch { }
        }

        public static List<Manga> Import(MangaSite site)
        {
            const int TOTAL_COLUMNS = 2;
            List<Manga> mangaList = new List<Manga>();
            Manga manga;
            List<string> columns;
            CsvTokenizer tokenizer = new CsvTokenizer();
            String mangaFilePath = String.Format("{0}\\{1}.csv", Application.StartupPath, site);
            try
            {
                using (StreamReader sr = new StreamReader(mangaFilePath, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        columns = tokenizer.Split(sr.ReadLine());
                        if (columns.Count == TOTAL_COLUMNS)
                        {
                            manga = new Manga();
                            manga.ID = Guid.NewGuid().ToString();
                            manga.Name = columns[0];
                            manga.Url = columns[1];
                            manga.Site = site;
                            mangaList.Add(manga);
                        }
                    }
                    sr.Close();
                }
            }
            catch { }
            return mangaList;
        }

        private class CsvTokenizer
        {
            private const char QUOTE = '"';
            private const char COMMA = ',';

            public CsvTokenizer() { }

            public List<String> Split(String line)
            {
                List<string> fixedValues = new List<string>();
                List<string> columns = new List<string>();
                Char charOfLine;
                int quoteStartIndex = -1,
                    quoteEndIndex = -1;
                string temp;

                for (int i = 0; i < line.Length; i++)
                {
                    charOfLine = line[i];

                    if (charOfLine == COMMA && quoteEndIndex > 0)
                    {
                        columns.Add(line.Substring(quoteStartIndex, i - quoteStartIndex));
                        quoteStartIndex = -1;
                        quoteEndIndex = -1;
                        continue;
                    }

                    if (charOfLine == QUOTE)
                    {
                        if (quoteStartIndex < 0)
                            quoteStartIndex = i;
                        else
                            quoteEndIndex = i;
                    }

                    if (i == line.Length - 1)
                        columns.Add(line.Substring(quoteStartIndex));
                }

                foreach (string value in columns)
                {
                    temp = value.Trim();
                    if (temp.IndexOf("\"") == 0)
                        temp = temp.Substring(1);
                    if (temp.LastIndexOf("\"") == temp.Length - 1)
                        temp = temp.Substring(0, temp.Length - 1);
                    fixedValues.Add(temp);
                }

                return fixedValues;
            }
        }
    }
}
