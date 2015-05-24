using MangaDownloader.Settings;
using MangaDownloader.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class Converter : BaseForm
    {
        private const string COLUMN_FILENAME = "cName";
        private const string COLUMN_PATH = "cPath";
        private const string PHOTO_FILTERS = "All|*.bmp;*.jpg;*.jpeg;*.jpe;*.giff;*.png;*.tif;*.tiff|Bitmap|*.bmp|JPEG|*.jpg;*.jpeg;*.jpe|GIFF|*.giff|PNG|*.png|TIFF|*.tif;*.tiff";

        public Converter()
        {
            InitializeComponent();
        }

        private void Converter_Load(object sender, EventArgs e)
        {
            
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "PDF|*.pdf";
            sf.InitialDirectory = SettingsManager.GetInstance().GetCommonSettings().RootDownloadFolderPath;
            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbSaveTo.Text = sf.FileName;
        }

        private void tssbtAddFiles_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = PHOTO_FILTERS;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                foreach (string path in ofd.FileNames)
                    dgvPhotos.Rows.Add(Path.GetFileName(path), path);

            UpdateLabelStatus();
            EnableOrNotButtons();
        }

        private void tsmiAddFolders_Click(object sender, EventArgs e)
        {

        }

        private void tsbtMoveUp_Click(object sender, EventArgs e)
        {
            List<int> selectedIndex = new List<int>();

            foreach (DataGridViewRow row in dgvPhotos.SelectedRows)
                selectedIndex.Add(row.Index);
            selectedIndex.Sort();

            foreach (int index in selectedIndex)
                if (index > 0)
                    Swap(index, index - 1);

            dgvPhotos.ClearSelection();
            foreach (int index in selectedIndex)
                dgvPhotos.Rows[index >= 1 ? index - 1 : index].Selected = true;

            EnableOrNotButtons();
        }

        private void tsbtMoveDown_Click(object sender, EventArgs e)
        {
            List<int> selectedIndex = new List<int>();
            int maxRowIndex = dgvPhotos.RowCount - 1;

            foreach (DataGridViewRow row in dgvPhotos.SelectedRows)
                selectedIndex.Add(row.Index);
            selectedIndex.Sort();
            selectedIndex.Reverse();

            foreach (int index in selectedIndex)
                if (index < dgvPhotos.RowCount - 1)
                    Swap(index, index + 1);

            dgvPhotos.ClearSelection();
            foreach (int index in selectedIndex)
                dgvPhotos.Rows[index < maxRowIndex ? index + 1 : index].Selected = true;

            EnableOrNotButtons();
        }

        private void dgvPhotos_KeyUp(object sender, KeyEventArgs e)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dgvPhotos.SelectedRows)
                rows.Add(r);
            foreach (DataGridViewRow r in rows)
                dgvPhotos.Rows.Remove(r);
            EnableOrNotButtons();
            UpdateLabelStatus();
        }

        private void tsbtToPDF_Click(object sender, EventArgs e)
        {
            if (dgvPhotos.RowCount == 0)
            {
                MessageBox.Show("No photo to ouput.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrEmpty(tbSaveTo.Text) || !Directory.Exists(Path.GetDirectoryName(tbSaveTo.Text)))
            {
                btBrowse_Click(sender, e);
            }

            List<string> rows = new List<string>();
            foreach (DataGridViewRow r in dgvPhotos.Rows)
                if (File.Exists(r.Cells[COLUMN_PATH].Value.ToString()))
                    rows.Add(r.Cells[COLUMN_PATH].Value.ToString());

            SetLabelStatus("Converting to PDF...");
            EnableOrDisableButtons(false);
            new Thread(new ParameterizedThreadStart((object list) =>
            {
                try
                {
                    List<string> images = (List<string>)list;
                    FileUtils.ImagesToPDF(images.ToArray(), tbSaveTo.Text);
                    SetLabelStatus("Complete");
                    EnableOrDisableButtons(true);
                    ShowMessageBox();
                }
                catch { }
            })).Start(rows);
        }

        private void UpdateLabelStatus()
        {
            if (dgvPhotos.RowCount > 0)
                SetLabelStatus(String.Format("{0} file(s) added", dgvPhotos.RowCount));
            else
                SetLabelStatus("");
        }

        private void SetLabelStatus(String msg)
        {
            try { statusStrip.Invoke(new MethodInvoker(() => { tsslbStatus.Text = msg; })); }
            catch { }
        }

        private void EnableOrNotButtons()
        {
            try
            {
                toolStrip.Invoke(new MethodInvoker(() =>
                {
                    if (dgvPhotos.SelectedRows.Count == 0)
                    {
                        tsbtMoveUp.Enabled = false;
                        tsbtMoveDown.Enabled = false;
                        tsbtToPDF.Enabled = false;
                    }
                    else
                    {
                        tsbtMoveUp.Enabled = true;
                        tsbtMoveDown.Enabled = true;
                        tsbtToPDF.Enabled = true;
                    }

                }));
            }
            catch { }
        }

        private void EnableOrDisableButtons(bool enabled)
        {
            try
            {
                toolStrip.Invoke(new MethodInvoker(() =>
                {
                    tssbtAddFiles.Enabled = enabled;
                    tsmiAddFolders.Enabled = enabled;
                    tsbtMoveUp.Enabled = enabled;
                    tsbtMoveDown.Enabled = enabled;
                    tsbtToPDF.Enabled = enabled;
                }));

                btBrowse.Invoke(new MethodInvoker(() => { btBrowse.Enabled = enabled; }));
                tbSaveTo.Invoke(new MethodInvoker(() => { tbSaveTo.Enabled = enabled; }));
                dgvPhotos.Invoke(new MethodInvoker(() => { dgvPhotos.Enabled = enabled; }));
            }
            catch { }
        }

        private void Swap(int fromIndex, int toIndex)
        {
            DataGridViewRow fromRow = dgvPhotos.Rows[fromIndex];
            DataGridViewRow toRow = dgvPhotos.Rows[toIndex];

            dgvPhotos.Rows.Remove(fromRow);
            dgvPhotos.Rows.Remove(toRow);
            if (fromIndex < toIndex)
            {
                // Move down
                dgvPhotos.Rows.Insert(fromIndex, toRow);
                dgvPhotos.Rows.Insert(toIndex, fromRow);
            }
            else
            {
                // Move up
                dgvPhotos.Rows.Insert(toIndex, fromRow);
                dgvPhotos.Rows.Insert(fromIndex, toRow);
            }
        }

        private void ShowMessageBox()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                MessageBox.Show("Hallo");
            }));
        }
    }
}
