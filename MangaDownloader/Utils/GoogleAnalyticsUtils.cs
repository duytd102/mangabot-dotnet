using MangaDownloader.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using WebScraper.Enums;
using WebScraper.Utils;

namespace MangaDownloader.Utils
{
    class GoogleAnalyticsUtils
    {
        private static String GA_URL = Properties.Settings.Default.GaBaseURL;
        private static String TRACKING_ID = Properties.Settings.Default.GaTrackingID;
        private static String CID = Properties.Settings.Default.GaClientID;
        private static String APPLICATION_NAME = Properties.Settings.Default.AppName;
        private static String APPLICATION_VERSION = String.Format("{0:0.0}", Properties.Settings.Default.AppVersion);

        static GoogleAnalyticsUtils()
        {
            if (String.IsNullOrEmpty(CID))
            {
                Properties.Settings.Default.GaClientID = Guid.NewGuid().ToString();
                Properties.Settings.Default.Save();
            }
            CID = Properties.Settings.Default.GaClientID;
        }

        public static void SendEvent(MangaSite site, EventAction eventAction, String url)
        {
            NameValueCollection values = GetDefaultParams();
            values["t"] = "event";                      // HIT TYPE
            values["ec"] = site.ToString();             // EVENT CATEGORY
            values["ea"] = eventAction.ToString();      // EVENT ACTION
            values["el"] = url;                         // EVENT LABEL

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        public static void SendError(Exception e)
        {
            NameValueCollection values = GetDefaultParams();
            values["t"] = "exception";      // HIT TYPE
            values["exd"] = e.Message;      // EXCEPTION DESCRIPTION

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        public static void SendView()
        {
            NameValueCollection values = GetDefaultParams();
            values["t"] = "screenview";     // HIT TYPE
            values["cd"] = "Main";          // SCREEN VIEW

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        private static NameValueCollection GetDefaultParams()
        {
            NameValueCollection values = new NameValueCollection();
            values["v"] = "1";                      // VERSION
            values["tid"] = TRACKING_ID;            // TRACKING ID
            values["cid"] = CID;                    // CLIENT ID

            values["an"] = APPLICATION_NAME;        // APPLICATION NAME
            values["av"] = APPLICATION_VERSION;     // APPLICATION VERSION

            return values;
        }

        private static String collection2String(NameValueCollection values)
        {
            string str = "";
            foreach (string item in values)
            {
                str += str.Length > 0 ? "&" : "";
                str += item + "=" + WebUtility.HtmlEncode(values[item]);
            }
            return str;
        }
    }
}
