using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class ZipUtils
    {
        public static void UnZip(String filePath)
        {
            string extractToPath = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath);
            using (var zf = Ionic.Zip.ZipFile.Read(filePath))
            {
                zf.ToList().ForEach(entry =>
                {
                    if (!entry.IsDirectory)
                    {
                        string folder = Path.GetDirectoryName(entry.FileName);
                        string fn = entry.FileName;
                        if (!String.IsNullOrEmpty(folder)) fn = fn.Substring(fn.IndexOf("/") + 1);
                        entry.FileName = fn;
                        entry.Extract(extractToPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                });
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

        public static void ZipFiles(string[] files, string zipToFile)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.ZipErrorAction = ZipErrorAction.Skip;
                for (int i = 0; i < files.Length; i++)
                {
                    string folder = Path.GetDirectoryName(files[i]);
                    string fileName = Path.GetFileName(files[i]);
                    zip.AddFile(files[i], "").FileName = Common.StringUtils.GenerateOrdinal(files.Length, i) + "_" + fileName;
                }
                zip.Save(zipToFile);
            }
        }
    }
}
