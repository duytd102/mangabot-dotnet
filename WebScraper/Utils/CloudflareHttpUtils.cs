using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace WebScraper.Utils
{
    /**
     * https://github.com/Anorov/cloudflare-scrape
     * https://gist.github.com/fmj02/65a4a97a9f29654945fc16fc703328de
     */
    class CloudflareHttpUtils
    {
        private static readonly string[] DEFAULT_USER_AGENTS =
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:46.0) Gecko/20100101 Firefox/46.0",
            "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:41.0) Gecko/20100101 Firefox/41.0"
        };

        string __cfduid = "";
        Dictionary<string, string> responseStorage = new Dictionary<string, string>();

        public string Get(string url, Dictionary<string, string> queries)
        {
            try
            {
                return Response2String(Request(url, queries, null));
            }
            catch(WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                Console.WriteLine("StatusCode: " + response.StatusCode);
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable && "cloudflare-nginx".Equals(response.Server, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (response.Cookies["__cfduid"] != null)
                    {
                        __cfduid = response.Cookies["__cfduid"].Value;
                        Console.WriteLine("__cfduid: " + __cfduid);
                    }

                    return Wait4CfValidate(response);
                }
                
                throw e;
            }
            catch (Exception e)
            {

            }
            return "";
        }

        private string Wait4CfValidate(HttpWebResponse cfRes)
        {
            Console.WriteLine("Wait 5s");
            Thread.Sleep(5000);     // Cloudflare requires a delay before solving the challenge

            string body = Response2String(cfRes);
            string domain = string.Format("{0}://{1}/", cfRes.ResponseUri.Scheme, cfRes.ResponseUri.Host);
            string host = cfRes.ResponseUri.Host;
            string submitUrl = string.Format("{0}://{1}/cdn-cgi/l/chk_jschl", cfRes.ResponseUri.Scheme, cfRes.ResponseUri.Host);

            Console.WriteLine("Host: " + host);
            Console.WriteLine("Submit URL: " + submitUrl);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers["Referer"] = cfRes.ResponseUri.AbsoluteUri;

            Dictionary<string, string> queries = new Dictionary<string, string>();
            queries["jschl_vc"] = Regex.Match(body, "name=\"jschl_vc\" value=\"(\\w+)\"").Groups[1].Value;
            queries["pass"] = Regex.Match(body, "name=\"pass\" value=\"(.+?)\"").Groups[1].Value;
            queries["jschl_answer"] = EvalJs(ExtractJs(host, body));

            Console.WriteLine("jschl_vc: " + queries["jschl_vc"]);
            Console.WriteLine("pass: " + queries["pass"]);
            Console.WriteLine("jschl_answer: " + queries["jschl_answer"]);

            try
            {
                return Response2String(Request(submitUrl, queries, headers));
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                Console.WriteLine("StatusCode: " + response.StatusCode);
                if (response.StatusCode == HttpStatusCode.MovedPermanently && "cloudflare-nginx".Equals(response.Server, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (response.Cookies["cf_clearance"] != null)
                    {
                        Properties.Cloudflare.Default.KISSMANGA = response.Cookies["cf_clearance"].Value;
                        Properties.Cloudflare.Default.Save();

                        Console.WriteLine("cf_clearance: " + Properties.Cloudflare.Default.KISSMANGA);
                    }

                    return Response2String(Request(cfRes.ResponseUri.AbsoluteUri, queries, headers));
                }

                throw e;
            }
            catch (Exception e)
            {

            }

            return "";
        }

        private string ExtractJs(string domain, string body)
        {
            string func = Regex.Match(body, @"setTimeout\(function\(\)\{\s+(.+?a\.value\s*=.+?\))").Value;
            string vars = Regex.Match(func, @"var\s+.*?;").Value;
            string calc = Regex.Match(func, @"\w{2,}\.\w+\s*[\+\-\*]?=.+").Value + "+" + domain.Length;
            string exec = Regex.Replace(calc, @"a.value\s*=", "return ");
            return "(function () {" + vars + exec + "})();";
        }

        private string EvalJs(string js)
        {
            var engine = new Jurassic.ScriptEngine();
            return engine.Evaluate(js).ToString();
        }

        private string AppendQueries(string url, Dictionary<string, string> queries)
        {
            string furl = url, queryString = "";

            if (queries != null)
            {
                foreach (string k in queries.Keys)
                {
                    queryString = string.Format("{0}&{1}={2}", queryString, k, queries[k]);
                }

                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    furl = string.Format("{0}?{1}", furl, queryString);
                }
            }

            return furl;
        }

        private HttpWebResponse Request(string url, Dictionary<string, string> queries, Dictionary<string, string> headers)
        {
            string finalUrl = AppendQueries(url, queries);
            Console.WriteLine("GET: " + finalUrl);

            Uri target = new Uri(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(finalUrl);
            request.Method = "GET";
            request.UserAgent = RandomUserAgent();
            request.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.CookieContainer = new CookieContainer();
            
            if (!string.IsNullOrWhiteSpace(__cfduid))
            {
                request.CookieContainer.Add(new Cookie("__cfduid", __cfduid) { Domain = target.Host });
            }

            if (!string.IsNullOrWhiteSpace(Properties.Cloudflare.Default.KISSMANGA))
            {
                request.CookieContainer.Add(new Cookie("cf_clearance", Properties.Cloudflare.Default.KISSMANGA) { Domain = target.Host });
            }

            if (headers != null)
            {
                foreach(string k in headers.Keys)
                {
                    if (k == "Referer")
                    {
                        request.Referer = headers["Referer"];
                    } else
                    {
                        request.Headers.Add(k, headers[k]);
                    }
                }
            }
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Properties.Cloudflare.Default.KISSMANGA = response.Cookies["cf_clearance"].Value;
            Properties.Cloudflare.Default.Save();
            
            return response;
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

        private static string RandomUserAgent()
        {
            int index = new Random().Next(0, DEFAULT_USER_AGENTS.Length);
            return DEFAULT_USER_AGENTS[index];
        }
    }
}
