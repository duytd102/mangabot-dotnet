﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Data;
using WebScraper.Enums;

namespace MangaDownloader.Utils
{
    class MangaUtils
    {
        public static void Export(MangaSite site, List<Manga> mangaList)
        {
            try
            {
                string folder = Application.StartupPath + "\\data";
                Directory.CreateDirectory(folder);

                String mangaFilePath = folder + "\\" + site.ToString().ToLower();
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
            try
            {
                List<Manga> mangaList = new List<Manga>();
                Manga manga;

                String mangaFilePath = Application.StartupPath + "\\data\\" + site.ToString().ToLower();
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

                return mangaList;
            }
            catch
            {
                return new List<Manga>();
            }
        }

        public static Bitmap GetLogo(MangaSite site)
        {
            switch (site)
            {
                case MangaSite.BLOGTRUYEN:
                    return Properties.Resources.blogtruyen_logo;

                case MangaSite.VECHAI:
                    return Properties.Resources.vechai_logo;

                case MangaSite.MANGAFOX:
                    return Properties.Resources.mangafox_logo;

                case MangaSite.MANGAVN:
                    return Properties.Resources.mangavn_logo;

                case MangaSite.MANGA24H:
                    return Properties.Resources.manga24h_logo;

                case MangaSite.TRUYENTRANHTUAN:
                    return Properties.Resources.truyentranhtuan_logo;

                case MangaSite.TRUYENTRANHNHANH:
                    return Properties.Resources.truyentranhnhanh_logo;

                case MangaSite.TRUYENTRANH8:
                    return Properties.Resources.truyentranh8_logo;

                case MangaSite.IZMANGA:
                    return Properties.Resources.izmanga_logo;

                default:
                    // TODO Add logo for more sites
                    throw new NotImplementedException();
            }
        }
    }
}