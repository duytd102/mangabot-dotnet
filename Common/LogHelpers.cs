using Common.Enums;
using System;
using System.IO;
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
            try
            {
                filePath = AppDomain.CurrentDomain.BaseDirectory + "\\debug.log";
            }
            catch { }
        }

        public static void Log(string msg)
        {
            try
            {
                if (CommonSettings.AppMode != AppMode.PROD)
                {
                    using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(Regex.Replace(msg, "(\\r|\\n|\\t|\\s{4})+", ""));
                        sw.Close();
                    }
                }
            }
            catch { }
        }
    }
}
