using Common;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class AutoUpdate : Form
    {
        Thread mainThread;
        public VersionData VersionData;

        public AutoUpdate()
        {
            InitializeComponent();
        }

        private void AutoUpdate_Load(object sender, EventArgs e)
        {
            mainThread = new Thread(() =>
            {
                if (VersionUtils.CheckForUpdates(out VersionData))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }

                try
                {
                    this.Invoke(new MethodInvoker(() =>  { this.Close(); }));
                }
                catch { }
            });
            mainThread.Start();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            btCancel.Enabled = false;
            try { mainThread.Abort(); }
            catch { }
            finally
            {
                btCancel.Enabled = true;
                this.Close();
            }
        }
    }
}
