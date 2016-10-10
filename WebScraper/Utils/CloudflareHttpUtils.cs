using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebScraper.Utils
{
    class CloudflareHttpUtils
    {
        private readonly string[] DEFAULT_USER_AGENTS =
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:46.0) Gecko/20100101 Firefox/46.0",
            "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:41.0) Gecko/20100101 Firefox/41.0"
        };

        public string Get(string url, Dictionary<string, string> queries)
        {
            try
            {
                string finalUrl = AppendQueries(url, queries);
                HttpWebResponse httpWebResponse = Request(url);

                if (httpWebResponse.StatusCode == HttpStatusCode.ServiceUnavailable
                    && "cloudflare-nginx".Equals(httpWebResponse.Server, StringComparison.CurrentCultureIgnoreCase))
                {
                    return WaitCloudflareValidation(httpWebResponse);
                }
                else
                {
                    return Response2String(httpWebResponse);
                }
            }
            catch { }
            return "";
        }

        private string WaitCloudflareValidation(HttpWebResponse cfRes)
        {
            Thread.Sleep(5000);     // Cloudflare requires a delay before solving the challenge

            string body = Response2String(cfRes);
            string submitUrl = string.Format("{0}://{1}/cdn-cgi/l/chk_jschl", cfRes.ResponseUri.Scheme, cfRes.ResponseUri.Host);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers["Referer"] = cfRes.ResponseUri.AbsolutePath;

            Dictionary<string, string> queries = new Dictionary<string, string>();
            queries["jschl_vc"] = Regex.Match("name=\"jschl_vc\" value=\"(\\w+)\"", body).group(1)
        }

        private string AppendQueries(string url, Dictionary<string, string> queries)
        {
            string furl = url, queryString = "";

            foreach (string k in queries.Keys)
            {
                queryString = string.Format("{0}&{1}={2}", queryString, k, queries[k]);
            }

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                furl = string.Format("{0}?{1}", furl, queryString);
            }

            return furl;
        }

        private HttpWebResponse Request(string url)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.UserAgent = RandomUserAgent();
            httpRequest.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            return (HttpWebResponse)httpRequest.GetResponse();
        }

        private string Response2String(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();

            StringBuilder sb = new StringBuilder();
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line);
                }
            }

            return sb.ToString();
        }
        
        public static string DoGetWithDecompression(string url, string queryString = "")
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
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
                httpRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

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

        public static string MakeHttpGet(string url, Dictionary<String, String> headers, string queryString = "")
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
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
                //httpRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                foreach (KeyValuePair<string, string> pair in headers)
                    httpRequest.Headers.Add(pair.Key, pair.Value);

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

        private static string RandomUserAgent()
        {
            int index = new Random().Next(0, DEFAULT_USER_AGENTS.Length);
            return DEFAULT_USER_AGENTS[index];
        }
    }
}
