using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public class BaseForm : Form
    {
        public BaseForm()
            : base()
        {
            this.Font = new System.Drawing.Font("Tahoma", 9);
        }
    }
}
