using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class VersionUtils
    {
        public static bool CheckForUpdates(out VersionData newVersion)
        {
            string currentVersion = CommonSettings.AppVersion();
            String versionUrl = CommonSettings.CheckVersionUrl();
            string response = HttpUtils.MakeHttpGetWithAppendLine(versionUrl);
            List<VersionData> list = Read(response);
            if (list.Count > 0)
            {
                VersionData v = list[0];
                if (CompareVersion(currentVersion, v.Version) == -1)
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
                        data.Version = csv.GetField<String>(0);
                        data.ReleaseDate = csv.GetField<DateTime>(1);
                        data.URL = csv.GetField(2);
                        list.Add(data);
                    }
                }
            }
            catch { }
            return list;
        }

        public static void Write(String folderPath, List<VersionData> list)
        {
            try
            {
                String mangaFilePath = folderPath + "\\version.csv";
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

        private static int CompareVersion(String src, String compareWith)
        {
            int SMALLER = -1;
            int EQUALS = 0;
            int LARGER = 1;

            string[] srcParts = src.Split('.');
            string[] cwp = compareWith.Split('.');
            int len = srcParts.Length > cwp.Length ? cwp.Length : srcParts.Length;
            for (int i = 0; i < len; i++)
            {
                int sd = int.Parse(srcParts[i]);
                int cd = int.Parse(cwp[i]);
                if (sd < cd) return SMALLER;
                else if (sd > cd) return LARGER;
            }

            if (srcParts.Length < cwp.Length) return SMALLER;
            else if (srcParts.Length > cwp.Length) return LARGER;

            return EQUALS;
        }
    }

    public class VersionData
    {
        public String Version;
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
    }
}
