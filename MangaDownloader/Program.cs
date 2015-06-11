using MangaDownloader.GUIs;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace MangaDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool run = false;
            Process process = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(process.ProcessName);
            foreach (Process p in processes)
            {
                // Get the first instance that is not this instance, has the
                // same process name and was started from the same file name
                // and location. Also check that the process has a valid
                // window handle in this session to filter out other user's
                // processes (p.MainWindowHandle != IntPtr.Zero).

                //MessageBox.Show(p.Id + " - " + p.MainModule.FileName + " - " + p.MainWindowHandle);

                if (p.Id != process.Id && p.MainModule.FileName == process.MainModule.FileName)
                {
                    run = true;
                    break;
                }
            }

            if (!run)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ThreadException += new ThreadExceptionEventHandler(GlobalExceptionCatcher.UnhandledThreadExceptionHandler);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("Manga Downloader is running, please check it in taskbar.", "Manga Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
