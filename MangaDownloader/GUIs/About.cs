using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class About : BaseForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lbVersion.Text = String.Format("Version: {0:0.0}", Properties.Settings.Default.AppVersion);
            lbRelease.Text = String.Format("Release: {0:yyyy-MM-dd}", Properties.Settings.Default.ReleaseDate);
            tbDescription.Text = GetDesc();
        }

        private String GetDesc()
        {
            var settings = Properties.Settings.Default;
            StringBuilder bd = new StringBuilder();
            bd.AppendLine("A tool for downloading manga from the internet.");
            bd.AppendLine("It's TOTALLY FREE 4EVER (NO ADS).");
            bd.AppendLine("Feel free to feedback to me any errors or more features.");
            bd.AppendLine();
            bd.AppendLine("=== Contact ===");
            bd.AppendLine("Author: " + settings.Author);
            bd.AppendLine("Email: " + settings.ContactEmail);
            bd.AppendLine("Website: " + settings.Website1);
            bd.AppendLine("          or: " + settings.Website2);
            bd.AppendLine();
            bd.AppendLine("=== Changelogs ===");
            bd.AppendLine("v1.0 (2015-03-22)");
            bd.AppendLine("Support blogtruyen.com, mangafox.in");
            return bd.ToString();
        }
    }
}
