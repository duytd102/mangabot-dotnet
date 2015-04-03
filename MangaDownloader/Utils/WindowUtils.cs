using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Utils
{
    class WindowUtils
    {
        public static void TurnOffComputer()
        {
            try { System.Diagnostics.Process.Start("shutdown", "/s /t 10"); }
            catch { }
        }
    }
}
