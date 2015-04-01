using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebScraper.Utils
{
    public class HttpUtils
    {
        /// <summary>
        /// Lấy page source của một trang bằng POST
        /// </summary>
        /// <param name="url">Đường dẫn đến trang</param>
        /// <param name="data">Dữ liệu gửi kèm lên server. Có dạng JSON.</param>
        /// <returns></returns>
        public static string MakeHttpPost(string url, string data = "")
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

                byte[] bytedata = Encoding.UTF8.GetBytes(data);
                httpRequest.ContentLength = bytedata.Length;

                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytedata, 0, bytedata.Length);
                requestStream.Close();

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }
                return sb.ToString();
            }
            catch { }
            return "";
        }

        public static string MakeHttpPost(string url, Hashtable dataModel)
        {
            string p = "";
            IEnumerator ie = dataModel.Keys.GetEnumerator();
            while(ie.MoveNext())
            {
                string key = ie.Current as string;
                p += p.Length > 0 ? "&" : "";
                p += String.Format("{0}={1}", key, dataModel[key].ToString());
            }
            return MakeHttpPost(url, p);
        }

        /// <summary>
        /// Lấy page source của trang bằng GET
        /// </summary>
        /// <param name="url">Đường dẫn đến trang</param>
        /// <param name="queryString">Query string gửi kèm đến trang (không chứa ký tự ?).
        ///                             Có dạng: name1=value1&amp;name2=value2&amp;...
        /// </param>
        /// <returns></returns>
        public static string MakeHttpGet(string url, string queryString = "")
        {
            try
            {
                // Combine url and queryString
                if (!String.IsNullOrEmpty(queryString))
                    url = String.Format("{0}?{1}", url, queryString);

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";
                //httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }

                return sb.ToString();
            }
            catch { }
            return "";
        }

        public static string MakeHttpGetWithAppendLine(string url, string queryString = "")
        {
            try
            {
                // Combine url and queryString
                if (!String.IsNullOrEmpty(queryString))
                    url = String.Format("{0}?{1}", url, queryString);

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";
                //httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }

                return sb.ToString();
            }
            catch { }
            return "";
        }

        /// <summary>
        /// Download file từ một đường dẫn và lưu trữ.
        /// </summary>
        /// <param name="url">Đường dẫn file cần download</param>
        /// <param name="path">Đường dẫn lưu file sau khi download</param>
        public static void DownloadFile(string url, string path)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    int index = url.IndexOf("?");
                    // Nếu url có query string => xóa QS trên url và tạo QS cho WebClient
                    if (index > 0)
                    {
                        #region Kiểm tra xem URL có sử dụng proxy không. Nếu có => không cắt proxy

                        // Lấy đoạn URL đầu tiên
                        string proxy = url.Substring(0, index);

                        // Kiểm tra xem có phải proxy không. Nếu phải => tìm index tiếp theo
                        if (proxy.EndsWith("proxy", StringComparison.InvariantCultureIgnoreCase))
                            index = url.IndexOf("?", index + 1);

                        #endregion

                        if (index > 0)
                        {
                            // Tạo query string
                            NameValueCollection nvc = new NameValueCollection();
                            string queryString = url.Substring(index + 1);
                            string[] qs = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string q in qs)
                            {
                                string[] nv = q.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                if (nv.Length == 2) nvc.Add(nv[0], nv[1]);
                            }

                            webClient.QueryString.Add(nvc);

                            // Xóa đoạn query string trên url
                            url = url.Remove(index);
                        }
                    }

                    // Download file
                    webClient.DownloadFile(url, path);
                }
            }
            catch { }
        }

        public static String CombineUrl(String path1, String path2)
        {
            String fixedPath1 = path1.Trim();
            String fixedPath2 = path2.Trim();
            if (fixedPath1.EndsWith("/"))
                fixedPath1 = fixedPath1.Remove(fixedPath1.Length - 1);
            if (fixedPath2.StartsWith("/"))
                fixedPath2 = fixedPath2.Remove(0, 1);
            return String.Format("{0}/{1}", fixedPath1, fixedPath2);
        }
    }
}
