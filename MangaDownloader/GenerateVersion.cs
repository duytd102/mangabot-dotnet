using MangaDownloader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader
{
    class GenerateVersion
    {
        public static void Generate()
        {
            List<VersionData> list = new List<VersionData>();
            for (int i = 0; i < 3; i++)
            {
                VersionData d = new VersionData();
                d.Version = "1.2";
                d.ReleaseDate = new DateTime(2015, 3, 25);
                d.URL = "http://www.mediafire.com/download/4qzrm5b43wa651v/[20140604]_Manga_BuZz_v1.3.rar";
                d.ChangeLogs = "Change 1<br>change2<br><br><br>change3";
                list.Add(d);
            }
            VersionUtils.Write(list);
        }
    }
}
