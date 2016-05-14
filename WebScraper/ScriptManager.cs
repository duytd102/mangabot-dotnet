using Common;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebScraper
{
    class ScriptManager
    {
        private static ScriptManager instance;
        private static List<string> scriptLinkRepo = new List<string>() {
            "https://drive.google.com/uc?export=download&id=0BwclU1yWN7VGbk53ck44RFJESms",
            "https://dl.dropboxusercontent.com/u/148375006/apps/mangabot/script-repo.csv"
        };
        private static Dictionary<MangaSite, List<string>> scriptLinks = new Dictionary<MangaSite, List<string>>();
        private static Dictionary<MangaSite, string> scriptSrcs = new Dictionary<MangaSite, string>();

        private ScriptManager()
        {
            foreach(string link in scriptLinkRepo)
            {
                string repoSrc = HttpUtils.MakeHttpGetWithAppendLine(link);
                if(string.IsNullOrWhiteSpace(repoSrc) == false)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(FileUtils.GenerateStreamFromString(repoSrc)))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] parts = line.Split(',');
                                if (parts.Length == 2)
                                {
                                    foreach (MangaSite s in EnumUtils.ToList<MangaSite>())
                                    {
                                        if (s.ToString().Equals(parts[0], StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            List<string> links = new List<string>();
                                            if (scriptLinks.ContainsKey(s))
                                            {
                                                links = scriptLinks[s];
                                            }

                                            links.Add(parts[1]);

                                            scriptLinks[s] = links;

                                            break;
                                        }
                                    }
                                }
                            }
                            sr.Close();
                        }
                    }
                    catch { }
                    break;
                }
            }
        }

        public static ScriptManager GetInstance()
        {
            if (instance == null) instance = new ScriptManager();
            return instance;
        }

        public string GetScript(MangaSite site)
        {
            if (scriptSrcs.ContainsKey(site))
            {
                return scriptSrcs[site];
            }
            else
            {
                if (scriptLinks.ContainsKey(site))
                {
                    List<string> links = scriptLinks[site];
                    foreach(string l in links)
                    {
                        string src = HttpUtils.MakeHttpGetWithAppendLine(l);
                        if (string.IsNullOrWhiteSpace(src) == false)
                        {
                            scriptSrcs.Add(site, src);
                            return src;
                        }
                    }
                }
            }
            return "";
        }
    }
}
