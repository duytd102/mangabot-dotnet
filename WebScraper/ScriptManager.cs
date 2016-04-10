using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebScraper
{
    class ScriptManager
    {
        private static ScriptManager instance;
        private static Dictionary<string, string> scripts = new Dictionary<string, string>();

        private ScriptManager() { }

        public static ScriptManager GetInstance()
        {
            if (instance == null) instance = new ScriptManager();
            return instance;
        }

        public string GetScript(string url)
        {
            if (scripts.ContainsKey(url) && String.IsNullOrWhiteSpace(scripts[url]) == false)
            {
                return scripts[url];
            }
            else
            {
                scripts.Add(url, HttpUtils.MakeHttpGet(url));
                return scripts[url];
            }
        }
    }
}
