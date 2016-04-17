using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class LogHelpers
    {
        private static string filePath = "";

        private LogHelpers() { }

        static LogHelpers()
        {
            filePath = AppDomain.CurrentDomain.BaseDirectory + "\\ .log";
        }

        public static void Log(string msg)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    sw.WriteLine(Regex.Replace(msg, "(\\r|\\n|\\t|\\s{4})+", ""));
                    sw.Close();
                }
            }
            catch { }
        }
    }
}
