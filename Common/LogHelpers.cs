using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class LogHelpers
    {
        private static string debugPath = "";
        private static string errorPath = "";

        private LogHelpers() { }

        static LogHelpers()
        {
            try
            {
                debugPath = "debug.log";
                errorPath = "errors.log";
            }
            catch { }
        }

        private static bool IsFileLogEnable()
        {
            //return CommonSettings.AppMode != AppMode.PROD;
            return true;
        }

        private static string AppendTime(string msg)
        {
            return DateTime.Now.ToString("yyyy-MM-dd") + ": " + Regex.Replace(msg, "(\\r|\\n|\\t|\\s{4})+", "");
        }

        private static void DeleteIfTooLarge(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                const long MB5 = 5 * 1000 * 1000;
                long size = fi.Length;
                if (size > MB5)
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
            }
        }

        public static void LogDebug(string msg)
        {
            try
            {
                if (IsFileLogEnable())
                {
                    DeleteIfTooLarge(debugPath);

                    using (StreamWriter sw = new StreamWriter(debugPath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(AppendTime(msg));
                        sw.Close();
                    }
                }
            }
            catch { }
        }

        public static void LogError(string msg)
        {
            try
            {
                if (IsFileLogEnable())
                {
                    DeleteIfTooLarge(errorPath);

                    using (StreamWriter sw = new StreamWriter(errorPath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(AppendTime(msg));
                        sw.Close();
                    }
                }
            }
            catch { }
        }
    }
}
