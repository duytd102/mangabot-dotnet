using Common;
using MangaDownloader.Enums;
using MangaDownloader.Utils;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebScraper.Data;
using WebScraper.Enums;

namespace MangaDownloader.GUIs
{
    public partial class AdvancedSearch : BaseForm
    {
        private const string COLUMN_LOGO = "cLogo";
        private const string COLUMN_NAME = "cName";
        private const string COLUMN_URL = "cUrl";
        private const string COLUMN_SITE = "cSite";

        private MainForm mainForm;
        private List<Manga> mangaList = new List<Manga>();
        private Loading loadingForm;

        public AdvancedSearch()
        {
            InitializeComponent();
        }

        public AdvancedSearch(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void AdvancedSearch_Load(object sender, EventArgs e)
        {
            dgvMangaList.AutoGenerateColumns = false;
            tbKeyword.Focus();

            Thread thread = new Thread(new ThreadStart(() => { ImportMangaList(); }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            string keyword = tbKeyword.Text;
            if (String.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("You should input keyword to search. Tips: You can search by name or url of manga", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            loadingForm = new Loading("Searching... please wait");
            loadingForm.Show();

            btSearch.Enabled = false;

            Thread thread = new Thread(new ThreadStart(() =>
            {
                List<Manga> filteredList = mangaList.Where(x => x.Name.Contains(keyword) || x.Url.Contains(keyword)).ToList();

                DataTable dt = CreateDataTable();
                foreach (Manga manga in filteredList)
                    dt.Rows.Add(manga.Site, manga.Name, manga.Url);

                dgvMangaList.Invoke(new MethodInvoker(() => {
                    dgvMangaList.DataSource = dt;
                }));

                Invoke(new MethodInvoker(() =>
                {
                    btSearch.Enabled = true;
                    loadingForm.Close();
                }));

            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void tbKeyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            btSearch_Click(sender, e);
        }

        private void cmsSearch_Opening(object sender, CancelEventArgs e)
        {
            if (dgvMangaList.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void tsmiDownload_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvMangaList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvMangaList.Rows[index];
                String name = row.Cells[COLUMN_NAME].Value.ToString();
                String url = row.Cells[COLUMN_URL].Value.ToString();
                MangaSite site = EnumUtils.Parse<MangaSite>(row.Cells[COLUMN_SITE].Value.ToString());
                Task task = this.mainForm.AddToQueue(name, LinkType.MANGA, site, url);
                this.mainForm.DownloadTask(task);
            }
        }

        private void tsmiAddToQueue_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvMangaList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow r = dgvMangaList.Rows[index];
                String name = r.Cells[COLUMN_NAME].Value.ToString();
                String url = r.Cells[COLUMN_URL].Value.ToString();
                MangaSite site = EnumUtils.Parse<MangaSite>(r.Cells[COLUMN_SITE].Value.ToString());
                this.mainForm.AddToQueue(name, LinkType.MANGA, site, url);
            }
        }

        private void tsmiCopyURL_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(dgvMangaList.SelectedRows[0].Cells[COLUMN_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvMangaList.SelectedRows[0].Cells[COLUMN_URL].Value.ToString()); }
            catch { }
        }

        private void dgvMangaList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.Button != MouseButtons.Right || rowIndex == -1)
                return;

            if (Control.ModifierKeys != Keys.Control && !dgvMangaList.Rows[rowIndex].Selected)
            {
                dgvMangaList.ClearSelection();
                dgvMangaList.Rows[rowIndex].Selected = true;
            }
        }

        private void ImportMangaList()
        {
            // TODO import manga list if has more site
            mangaList = new List<Manga>();
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.BLOGTRUYEN));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.IZMANGA));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.KISSMANGA));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.MANGA24H));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.MANGAFOX));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.MANGAVN));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.TRUYENTRANH8));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.TRUYENTRANHNHANH));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.TRUYENTRANHTUAN));
            mangaList.AddRange(MangaUtils.Import(WebScraper.Enums.MangaSite.VECHAI));
            mangaList.Sort(new Comparison<Manga>((Manga m1, Manga m2) => { return m1.Name.CompareTo(m2.Name); }));
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("site"));
            dt.Columns.Add(new DataColumn("name"));
            dt.Columns.Add(new DataColumn("url"));
            return dt;
        }
    }
}
