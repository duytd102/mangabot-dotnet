using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    static class Program
    {
        private static string MD_APP_NAME = "Manga Downloader.exe";
        private static string AUTO_UPDATE_APP_NAME = "Auto Update.exe";
        private static string path;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Thread.Sleep(5000);
                try
                {
                    path = args[0];
                    MoveToUpdate();
                    String newAutoUpdatePath = MoveAutoUpdate();
                    Directory.Delete(path, true);
                    Process.Start(MD_APP_NAME, newAutoUpdatePath);
                }
                catch { }
            }
        }

        static void MoveToUpdate()
        {
            string[] paths = Directory.GetFiles(path);
            foreach (string p in paths)
            {
                string fn = Path.GetFileName(p);
                if (!fn.Equals(AUTO_UPDATE_APP_NAME))
                    File.Copy(p, String.Format("{0}\\{1}", Application.StartupPath, fn), true);
            }

            string[] folders = Directory.GetDirectories(path);
            foreach (string f in folders)
            {
                string relativePath = f.Replace(path + "\\", "");
                string toPath = Application.StartupPath + "\\" + relativePath;
                if (Directory.Exists(toPath)) Directory.Delete(toPath, true);
                Directory.Move(f, toPath);
            }
        }

        static String MoveAutoUpdate()
        {
            string[] paths = Directory.GetFiles(path);
            foreach (string p in paths)
            {
                string fn = Path.GetFileName(p);
                if (fn.Equals(AUTO_UPDATE_APP_NAME))
                {
                    string newFn = String.Format("{0}\\{1}.tmp", Application.StartupPath, fn);
                    File.Copy(p, newFn, true);
                    return newFn;
                }
            }
            return "";
        }
    }
}
