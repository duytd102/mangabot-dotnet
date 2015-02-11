using MangaDownloader.Enums;
using MangaDownloader.Processors;
using MangaDownloader.Utils;
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
using WebScraper.Scrapers;
using WebScraper.Utils;

namespace MangaDownloader.GUIs
{
    public partial class MainForm : Form
    {
        const String COLUMN_MANGA_NO = "colMangaNo";
        const String COLUMN_MANGA_ID = "colMangaID";
        const String COLUMN_MANGA_NAME = "colMangaName";
        const String COLUMN_MANGA_URL = "colMangaUrl";
        const String COLUMN_MANGA_SITE = "colMangaSite";

        const String COLUMN_CHAPTER_NO = "colChapterNo";
        const String COLUMN_CHAPTER_ID = "colChapterID";
        const String COLUMN_CHAPTER_NAME = "colChapterName";
        const String COLUMN_CHAPTER_URL = "colChapterUrl";
        const String COLUMN_CHAPTER_SITE = "colChapterSite";
        const String COLUMN_CHAPTER_LINK_TYPE = "colChapterLinkType";

        BackgroundWorker mangaWorker;
        BackgroundWorker chapterWorker;
        BackgroundWorker pageWorker;
        MangaSite currentSite = MangaSite.BLOGTRUYEN;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            List<Manga> mangaList = MangaExportUtils.Import(currentSite);
            UpdateMangaListGridView(mangaList);
            initWorkers();
        }

        private void tsmiBlogTruyen_Click(object sender, EventArgs e)
        {
            currentSite = MangaSite.BLOGTRUYEN;
            tslbSiteName.Text = "Blog Truyen";
            tslbSiteName.Image = Properties.Resources.blogtruyen_logo;
        }

        private void tsmiMangaFox_Click(object sender, EventArgs e)
        {
            currentSite = MangaSite.MANGAFOX;
            tslbSiteName.Text = "Manga Fox";
            tslbSiteName.Image = Properties.Resources.blogtruyen_logo;
        }

        private void tsmiContactMe_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnRefreshMangaList_Click(object sender, EventArgs e)
        {
            if (mangaWorker.IsBusy) return;
            mangaWorker.RunWorkerAsync();
        }

        private void tsbtnStartAll_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnPauseAll_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnManga_Click(object sender, EventArgs e)
        {
            if (chapterWorker.IsBusy) return;

            tslbSlash.Visible = false;
            tslbChapter.Visible = false;

            ToolStripButton btnManga = (ToolStripButton)sender;
            String mangaUrl = btnManga.Tag.ToString();
            chapterWorker.RunWorkerAsync(mangaUrl);
        }

        private void dgvMangaList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (chapterWorker.IsBusy) return;
            if (dgvMangaList.SelectedRows.Count == 0) return;

            String mangaName = dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_NAME].Value.ToString();
            String mangaUrl = dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_URL].Value.ToString();

            tslbSlash.Visible = false;
            tslbChapter.Visible = false;
            tsbtnManga.Visible = true;
            tsbtnManga.Text = "Manga: " + mangaName;
            tsbtnManga.Tag = mangaUrl;

            chapterWorker.RunWorkerAsync(mangaUrl);
        }

        private void dgvChapterList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pageWorker.IsBusy) return;
            if (dgvChapterList.SelectedRows.Count == 0) return;

            LinkType linkType = EnumUtils.Parse<LinkType>(
                dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_LINK_TYPE].Value.ToString());
            String chapterName = dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_NAME].Value.ToString();
            String chapterUrl = dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_URL].Value.ToString();

            if (linkType == LinkType.CHAPTER)
            {
                tslbSlash.Visible = true;
                tslbChapter.Visible = true;
                tslbChapter.Text = "Chapter: " + chapterName;

                pageWorker.RunWorkerAsync(chapterUrl);
            }
        }

        private void initWorkers()
        {
            mangaWorker = new BackgroundWorker();
            mangaWorker.DoWork += mangaWorker_DoWork;
            mangaWorker.RunWorkerCompleted += mangaWorker_RunWorkerCompleted;

            chapterWorker = new BackgroundWorker();
            chapterWorker.DoWork += chapterWorker_DoWork;
            chapterWorker.RunWorkerCompleted += chapterWorker_RunWorkerCompleted;

            pageWorker = new BackgroundWorker();
            pageWorker.DoWork += pageWorker_DoWork;
            pageWorker.RunWorkerCompleted += pageWorker_RunWorkerCompleted;
        }

        void mangaWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IProcessor processor = ProcessorFactory.GetInstance(currentSite);
            e.Result = processor.GetMangaList();
        }

        void mangaWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<Manga> mangaList = (List<Manga>)e.Result;
            MangaExportUtils.Export(currentSite, mangaList);
            UpdateMangaListGridView(mangaList);
        }

        private void UpdateMangaListGridView(List<Manga> mangaList)
        {
            int currentRowIndex = 1;
            dgvMangaList.Rows.Clear();

            foreach (Manga manga in mangaList)
            {
                dgvMangaList.Rows.Add(StringUtils.GenerateOrdinal(mangaList.Count, currentRowIndex),
                    manga.ID, manga.Name, manga.Url, manga.Site.ToString());

                currentRowIndex++;
            }

            dgvMangaList.PerformLayout();
        }

        void chapterWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String mangaUrl = e.Argument.ToString();
            IProcessor processor = ProcessorFactory.GetInstance(currentSite);
            e.Result = processor.GetChapterList(mangaUrl);
        }

        void chapterWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int currentRowIndex = 1;
            List<Chapter> chapterList = (List<Chapter>)e.Result;

            dgvChapterList.Rows.Clear();

            foreach (Chapter chapter in chapterList)
            {
                dgvChapterList.Rows.Add(StringUtils.GenerateOrdinal(chapterList.Count, currentRowIndex),
                    chapter.ID, chapter.Name, chapter.Url, chapter.Site.ToString(), LinkType.CHAPTER);

                currentRowIndex++;
            }

            dgvChapterList.PerformLayout();
        }

        void pageWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String chapterUrl = e.Argument.ToString();
            IProcessor processor = ProcessorFactory.GetInstance(currentSite);
            e.Result = processor.GetPageList(chapterUrl);
        }

        void pageWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int currentRowIndex = 1;
            List<Page> pageList = (List<Page>)e.Result;

            dgvChapterList.Rows.Clear();

            foreach (Page page in pageList)
            {
                dgvChapterList.Rows.Add(StringUtils.GenerateOrdinal(pageList.Count, currentRowIndex),
                    page.ID, page.Name, page.Url, page.Site.ToString(), LinkType.PAGE);

                currentRowIndex++;
            }

            dgvChapterList.PerformLayout();
        }

        private void cmsMangaMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = (dgvMangaList.SelectedRows.Count == 0);
        }

        private void tsmiMangaViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiMangaCopyURL_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiMangaAddToQueue_Click(object sender, EventArgs e)
        {

        }

        private void cmsChapterMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = (dgvChapterList.SelectedRows.Count == 0);
        }

        private void tsmiChapterViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiChapterCopyURL_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiChapterAddToQueue_Click(object sender, EventArgs e)
        {

        }

        private void cmsTaskMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = (dgvTasks.SelectedRows.Count == 0);
        }
    }
}
