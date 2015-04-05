
using CsvHelper;
using MangaDownloader.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Utils;

namespace MangaDownloader.Utils
{
    class VersionUtils
    {
        public static bool CheckForUpdates(out VersionData newVersion)
        {
            SettingsManager sm = SettingsManager.GetInstance();
            String versionUrl = sm.GetSettings().VersionURL;
            double currentVersion = sm.GetSettings().AppVersion;

            string response = HttpUtils.MakeHttpGetWithAppendLine(versionUrl);
            List<VersionData> list = VersionUtils.Read(response);
            if (list.Count > 0)
            {
                VersionData v = list[0];
                if (currentVersion < v.Version)
                {
                    newVersion = v;
                    return true;
                }
            }
            newVersion = null;
            return false;
        }

        public static List<VersionData> Read(string response)
        {
            List<VersionData> list = new List<VersionData>();
            try
            {
                VersionData data;
                using (CsvReader csv = new CsvReader(new StringReader(response)))
                {
                    while (csv.Read())
                    {
                        data = new VersionData();
                        data.Version = csv.GetField<double>(0);
                        data.ReleaseDate = csv.GetField<DateTime>(1);
                        data.URL = csv.GetField(2);
                        list.Add(data);
                    }
                }
            }
            catch { }
            return list;
        }

        public static void Write(List<VersionData> list)
        {
            try
            {
                String mangaFilePath = Application.StartupPath + "\\version.csv";
                using (StreamWriter sw = new StreamWriter(mangaFilePath, false, Encoding.UTF8))
                {
                    var csv = new CsvWriter(sw);
                    csv.WriteField("Version");
                    csv.WriteField("Release Date");
                    csv.WriteField("URL");
                    csv.NextRecord();

                    foreach (VersionData v in list)
                    {
                        csv.WriteField(v.Version);
                        csv.WriteField(v.ReleaseDate.ToString("yyyy-MM-dd"));
                        csv.WriteField(v.URL);
                        csv.NextRecord();
                    }

                    sw.Close();
                }
            }
            catch { }
        }
    }

    public class VersionData
    {
        public double Version;
        public DateTime ReleaseDate;
        public String URL;
        public String ChangeLogs;

        public List<String> GetChangeLogs()
        {
            List<String> list = new List<string>();
            string[] arr = this.ChangeLogs.Split(new String[] { "<br>" }, StringSplitOptions.None);
            list.AddRange(arr);
            return list;
        }

        public String GetVersionString()
        {
            return WindowUtils.FormatDouble("{0:0.0}", this.Version);
        }
    }
}
