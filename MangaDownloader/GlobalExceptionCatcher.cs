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
            LogHelpers.LogError(e.Exception.Message + "#" + e.Exception.StackTrace);
            MessageBox.Show("Oops, something went wrong! Make sure to update the latest version or leave a comment to report bugs at http://mangabot.github.io. Thank you.", CommonSettings.AppName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
