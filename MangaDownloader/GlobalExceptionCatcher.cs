using MangaDownloader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MangaDownloader
{
    class GlobalExceptionCatcher
    {
        public static void UnhandledThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("An unexpected error has occurred. Please report it to me via email dangduy2910@gmail.com.", "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            GoogleAnalyticsUtils.SendError(e.Exception);
            Application.Exit();
        }
    }
}
