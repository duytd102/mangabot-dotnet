using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MangaDownloader.Utils
{
    class FileUtils
    {
        static List<string> imageExtensions = new List<string> { ".BMP", ".PNG", ".JPEG", ".JPG", ".GIF", ".TIFF" };

        /// <summary>
        /// Loại bỏ những ký tự không hợp lệ trong tên thư mục hoặc tập tin.
        /// </summary>
        /// <param name="name">Tên thư mục hoặc tập tin</param>
        /// <returns>Tên hợp lệ</returns>
        public static string GetSafeName(string name)
        {
            string safeName = name;
            char[] invalidChar = Path.GetInvalidFileNameChars();
            foreach (char i in invalidChar)
                safeName = safeName.Replace(i.ToString(), "");
            return safeName;
        }

        /// <summary>
        /// Lấy phần mở rộng của tập tin trong đường dẫn (có thể chứa query string).
        /// </summary>
        /// <param name="url">Đường dẫn đến tập tin</param>
        /// <returns>Phần mở rộng của tập tin</returns>
        public static string GetExtension(string url)
        {
            string extension = "";

            try { extension = Path.GetExtension(url); }
            catch (ArgumentException) { }

            int questionIndex = extension.IndexOf("?");
            if (questionIndex > 0)
                extension = extension.Substring(0, questionIndex);

            foreach (String ext in imageExtensions)
                if (extension.EndsWith(ext, StringComparison.CurrentCultureIgnoreCase))
                    return extension;

            return ".png";
        }

        /// <summary>
        /// Tạo shortcut để mở một đường link.
        /// </summary>
        /// <param name="url">Đường dẫn cần mở</param>
        /// <param name="folderPath">Đường dẫn thư mục cha chứa tập tin shortcut</param>
        /// <param name="fileName">Tên tập tin cần tạo (không chứa phần mở rộng)</param>
        public static void CreateShortcut(string url, string folderPath, string fileName)
        {
            string fn = String.IsNullOrEmpty(fileName) ? "Manga Downloader" : FileUtils.GetSafeName(fileName);
            using (StreamWriter writer = new StreamWriter(String.Format("{0}\\{1}.url", folderPath, fn)))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=" + url);
                writer.Flush();
                writer.Close();
            }
        }

        public static void ZipFolder(string folderPath, string fileNameWithoutExtension)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(folderPath);
                zip.ZipErrorAction = ZipErrorAction.Skip;
                zip.Save(String.Format("{0}\\{1}.zip",
                    Path.GetDirectoryName(folderPath),
                    FileUtils.GetSafeName(fileNameWithoutExtension)));
            }
        }

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
                        if (CheckIfImage(imagePath))
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
            catch (Exception)
            {
                // TODO usually is document has no page
            }
        }

        private static bool CheckIfImage(string path)
        {
            bool img = false;
            string extension = Path.GetExtension(path);
            if (imageExtensions.FindIndex(p => p.ToUpper() == extension.ToUpper()) >= 0)
                img = true;
            return img;
        }
    }
}
