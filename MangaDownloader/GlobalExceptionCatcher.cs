using Common;
using System.Threading;
using System.Windows.Forms;

namespace MangaDownloader
{
    class GlobalExceptionCatcher
    {
        public static void UnhandledThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            GoogleAnalyticsUtils.SendError(CommonSettings.AppName(), CommonSettings.AppVersion(), e.Exception);
            MessageBox.Show("An unexpected error has occurred. Please report it to me via email dangduy2910@gmail.com.", "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
