using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter
{
    class FileUtils
    {
        static List<string> imageExtensions = new List<string> { ".BMP", ".PNG", ".JPEG", ".JPG", ".GIF", ".TIFF" };

        public static void ImagesToPDF(string[] images, string pdfPath)
        {
            try
            {
                using (Document doc = new Document(PageSize.A4, 20, 20, 20, 20))
                {
                    PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                    doc.Open();
                    foreach (string imagePath in images)
                    {
                        if (isPhoto(imagePath))
                        {
                            doc.NewPage();
                            Image gif = Image.GetInstance(imagePath);
                            gif.Alignment = Image.ALIGN_CENTER | Image.ALIGN_MIDDLE;
                            gif.ScaleToFit(doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin),
                                doc.PageSize.Height - (doc.TopMargin + doc.BottomMargin));

                            doc.Add(gif);
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public static void ZipFiles(string[] files, string zipToFile)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.ZipErrorAction = ZipErrorAction.Skip;
                for (int i = 0; i < files.Length; i++)
                {
                    string folder = Path.GetDirectoryName(files[i]);
                    string fileName = Path.GetFileName(files[i]);
                    zip.AddFile(files[i], "").FileName = GenerateOrdinal(files.Length, i) + "_" + fileName;
                }
                zip.Save(zipToFile);
            }
        }

        public static bool isPhoto(string path)
        {
            bool img = false;
            string extension = Path.GetExtension(path);
            if (imageExtensions.FindIndex(p => p.ToUpper() == extension.ToUpper()) >= 0)
                img = true;
            return img;
        }

        public static string GenerateOrdinal(int totalRows, int currentRowIndex)
        {
            string name = "";
            int lengthOfName = totalRows.ToString().Length - currentRowIndex.ToString().Length;
            while (lengthOfName > 0)
            {
                name += "0";
                lengthOfName--;
            }
            name += currentRowIndex;
            return name;
        }
    }
}
