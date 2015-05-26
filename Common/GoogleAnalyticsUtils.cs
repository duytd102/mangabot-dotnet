using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Common
{
    public class GoogleAnalyticsUtils
    {
        private static String GA_URL = Common.Properties.AppSettings.Default.GaBaseURL;
        private static String TRACKING_ID = Common.Properties.AppSettings.Default.GaTrackingID;
        private static String CID = Common.Properties.AppSettings.Default.GaClientID;

        static GoogleAnalyticsUtils()
        {
            if (String.IsNullOrEmpty(CID))
            {
                Common.Properties.AppSettings.Default.GaClientID = Guid.NewGuid().ToString();
                Common.Properties.AppSettings.Default.Save();
            }
            CID = Common.Properties.AppSettings.Default.GaClientID;
        }

        public static void SendEvent(String appName, String appVersion, String site, EventAction eventAction, String url)
        {
            NameValueCollection values = GetDefaultParams(appName, appVersion);
            values["t"] = "event";                      // HIT TYPE
            values["ec"] = site;             // EVENT CATEGORY
            values["ea"] = eventAction.ToString();      // EVENT ACTION
            values["el"] = url;                         // EVENT LABEL

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        public static void SendError(String appName, String appVersion, Exception e)
        {
            NameValueCollection values = GetDefaultParams(appName, appVersion);
            values["t"] = "exception";      // HIT TYPE
            values["exd"] = e.Message + "#" + e.StackTrace;   // EXCEPTION DESCRIPTION

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        public static void SendView(String appName, String appVersion, String screen)
        {
            NameValueCollection values = GetDefaultParams(appName, appVersion);
            values["t"] = "screenview";     // HIT TYPE
            values["cd"] = screen;          // SCREEN VIEW

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        private static NameValueCollection GetDefaultParams(String appName, String appVersion)
        {
            NameValueCollection values = new NameValueCollection();
            values["v"] = "1";                      // VERSION
            values["tid"] = TRACKING_ID;            // TRACKING ID
            values["cid"] = CID;                    // CLIENT ID

            values["an"] = appName;                 // APPLICATION NAME
            values["av"] = appVersion;              // APPLICATION VERSION

            return values;
        }

        private static String collection2String(NameValueCollection values)
        {
            string str = "";
            foreach (string item in values)
            {
                str += str.Length > 0 ? "&" : "";
                str += item + "=" + HttpUtility.HtmlEncode(values[item]);
            }
            return str;
        }
    }
}
