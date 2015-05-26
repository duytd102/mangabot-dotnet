using Common;
using CsvHelper;
using MangaDownloader.Enums;
using MangaDownloader.Settings;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Enums;

namespace MangaDownloader.Utils
{
    class TaskUtils
    {
        public static void Export(List<Task> taskList)
        {
            try
            {
                string folder = Application.StartupPath + "\\data";
                Directory.CreateDirectory(folder);

                string taskPath = folder + "\\tasks";
                using (StreamWriter writer = new StreamWriter(taskPath, false, Encoding.UTF8))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteField("Name");
                    csv.WriteField("Url");
                    csv.WriteField("Type");
                    csv.WriteField("Site");
                    csv.WriteField("Status");
                    csv.WriteField("Percent");
                    csv.WriteField("Path");
                    csv.WriteField("Desc");
                    csv.NextRecord();

                    foreach (Task task in taskList)
                    {
                        csv.WriteField(task.Name);
                        csv.WriteField(task.Url);
                        csv.WriteField(task.Type);
                        csv.WriteField(task.Site);
                        csv.WriteField(task.Status);
                        csv.WriteField(task.Percent);
                        csv.WriteField(task.Path);
                        csv.WriteField(task.Description);
                        csv.NextRecord();
                    }

                    writer.Close();
                }
            }
            catch { }
        }

        public static List<Task> Import()
        {
            try
            {
                List<Task> taskList = new List<Task>();
                Task task;

                string taskPath = Application.StartupPath + "\\data\\tasks";
                using (StreamReader reader = new StreamReader(taskPath, Encoding.UTF8))
                {
                    var csv = new CsvReader(reader);
                    while (csv.Read())
                    {
                        task = new Task();
                        task.Name = csv.GetField<string>(0);
                        task.Url = csv.GetField<string>(1);
                        task.Type = EnumUtils.Parse<LinkType>(csv.GetField<string>(2));
                        task.Site = EnumUtils.Parse<MangaSite>(csv.GetField<string>(3));
                        task.Status = EnumUtils.Parse<TaskStatus>(csv.GetField<string>(4));
                        task.Percent = csv.GetField<double>(5);
                        task.Path = csv.GetField<string>(6);
                        task.Description = csv.GetField<string>(7);
                        taskList.Add(task);
                    }

                    reader.Close();
                }

                return taskList;
            }
            catch
            {
                return new List<Task>();
            }
        }
    }
}
