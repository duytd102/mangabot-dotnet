using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static string FormatDouble(string format, double d)
        {
            return String.Format(new CultureInfo("en-US", false), format, d);
        }
    }
}
