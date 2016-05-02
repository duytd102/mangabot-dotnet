using System;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class Loading : BaseForm
    {
        private string description = "";

        public Loading()
        {
            InitializeComponent();
        }

        public Loading(string desc)
        {
            InitializeComponent();
            description = desc;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            lbDescription.Text = description;
        }
    }
}
