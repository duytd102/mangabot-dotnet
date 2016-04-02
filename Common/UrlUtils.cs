using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class UrlUtils
    {
        public static string FixUrl(string domainOrBaseUrl, string url)
        {
            string fixedUrl = string.IsNullOrEmpty(url) ? "" : url.Trim();
            Uri root = new Uri(domainOrBaseUrl);

            if (fixedUrl.StartsWith("//"))
            {
                // truyentranh8.net
                // e.g: //truyentranh8.net/manga/one-shot.html
                return root.Scheme + ":" + fixedUrl;
            }
            else if (fixedUrl.StartsWith("../") || (fixedUrl.StartsWith("/") && fixedUrl.StartsWith("//") == false))
            {
                // e.g: ../manga/one-shot.html
                return root.Scheme + "://" + root.Host + fixedUrl.Replace("../", "/");
            }
            else if (fixedUrl.StartsWith(root.Scheme + "://" + root.Host) == false)
            {
                // Following links will relate with the path in <base> element.
                // e.g: manga/one-shot.html
                // manga24h.com
                return domainOrBaseUrl + fixedUrl;
            }
            else
            {
                return fixedUrl;
            }
        }
    }
}
