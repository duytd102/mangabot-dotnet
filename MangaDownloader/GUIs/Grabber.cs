using MangaDownloader.Enums;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Processors;
using WebScraper.Utils;

namespace MangaDownloader.GUIs
{
    public partial class Grabber : BaseForm
    {
        private const String COLUMN_NO = "cNo";
        private const String COLUMN_NAME = "cName";
        private const String COLUMN_URL = "cURL";
        private BackgroundWorker worker;
        private MangaSite currentSite;
        private MainForm mainForm;

        public Grabber()
        {
            InitializeComponent();
        }

        public Grabber(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Grabber_Load(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            String mangaUrl = Clipboard.GetText();
            if (DetectUrlUtils.IsSupportedSite(mangaUrl))
            {
                tbMangaURL.Text = mangaUrl;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            String mangaUrl = e.Argument.ToString();
            IProcessor processor = ProcessorFactory.CreateProcessor(currentSite);
            e.Result = processor.GetChapterList(mangaUrl);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int currentRowIndex = 1;
            List<Chapter> chapterList = (List<Chapter>)e.Result;

            dgvChapterList.Rows.Clear();

            foreach (Chapter chapter in chapterList)
            {
                dgvChapterList.Rows.Add(StringUtils.GenerateOrdinal(chapterList.Count, currentRowIndex), chapter.Name, chapter.Url);
                currentRowIndex++;
            }

            btGrab.Enabled = true;
            tbMangaURL.Enabled = true;
        }

        private void btGrab_Click(object sender, EventArgs e)
        {
            string mangaUrl = tbMangaURL.Text;

            if (String.IsNullOrEmpty(mangaUrl))
            {
                MessageBox.Show("Missing manga url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DetectUrlUtils.IsSupportedSite(mangaUrl))
            {
                MessageBox.Show("Currently I don't support this manga yet. Sorry for this inconvenient", "Unknown", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentSite = DetectUrlUtils.GetSite(mangaUrl);
            btGrab.Enabled = false;
            tbMangaURL.Enabled = false;
            worker.RunWorkerAsync(mangaUrl);
        }

        private void tsmiAddToQueue_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();

            foreach(DataGridViewRow row in dgvChapterList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow r = dgvChapterList.Rows[index];
                String name = r.Cells[COLUMN_NAME].Value.ToString();
                String url = r.Cells[COLUMN_URL].Value.ToString();
                this.mainForm.AddToQueue(name, LinkType.CHAPTER, currentSite, url);
            }
        }

        private void tsmiDownload_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();

            foreach(DataGridViewRow row in dgvChapterList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvChapterList.Rows[index];
                String name = row.Cells[COLUMN_NAME].Value.ToString();
                String url = row.Cells[COLUMN_URL].Value.ToString();
                Task task = this.mainForm.AddToQueue(name, LinkType.CHAPTER, currentSite, url);
                this.mainForm.DownloadTask(task);
            }
        }

        private void tsmiCopyURL_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(dgvChapterList.SelectedRows[0].Cells[COLUMN_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvChapterList.SelectedRows[0].Cells[COLUMN_URL].Value.ToString()); }
            catch { }
        }

        private void cmsChapterList_Opening(object sender, CancelEventArgs e)
        {
            if (dgvChapterList.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void dgvChapterList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.Button != MouseButtons.Right || rowIndex == -1)
                return;

            if (Control.ModifierKeys != Keys.Control && !dgvChapterList.Rows[rowIndex].Selected)
            {
                dgvChapterList.ClearSelection();
                dgvChapterList.Rows[rowIndex].Selected = true;
            }
        }
    }
}
