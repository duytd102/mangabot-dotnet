using System;
using System.Collections.Specialized;
using System.Web;

namespace Common
{
    public class GoogleAnalyticsUtils
    {
        private static string GA_URL = CommonSettings.GaBaseUrl();
        private static string TRACKING_ID = CommonSettings.GaTrackingId();
        private static string CID = "";

        static GoogleAnalyticsUtils()
        {
            ConfigurationData cd = ConfigurationIO.Read();

            CID = Properties.Settings.Default.GaClientID;

            if (string.IsNullOrWhiteSpace(CID))
            {
                CID = cd.ClientID;

                if (string.IsNullOrWhiteSpace(CID))
                {
                    CID = Guid.NewGuid().ToString().Replace("-", "");
                }
            }

            Properties.Settings.Default.GaClientID = CID;
            Properties.Settings.Default.Save();

            cd.ClientID = CID;
            ConfigurationIO.Write(cd);
        }

        public static void SendEvent(string appName, string appVersion, string site, EventAction eventAction, string url)
        {
            NameValueCollection values = GetDefaultParams(appName, appVersion);
            values["t"] = "event";                      // HIT TYPE
            values["ec"] = site;             // EVENT CATEGORY
            values["ea"] = eventAction.ToString();      // EVENT ACTION
            values["el"] = url;                         // EVENT LABEL

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        public static void SendError(string appName, string appVersion, Exception e)
        {
            NameValueCollection values = GetDefaultParams(appName, appVersion);
            values["t"] = "exception";      // HIT TYPE
            values["exd"] = e.Message + "#" + e.StackTrace;   // EXCEPTION DESCRIPTION

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        public static void SendView(string appName, string appVersion, string screen)
        {
            NameValueCollection values = GetDefaultParams(appName, appVersion);
            values["t"] = "screenview";     // HIT TYPE
            values["cd"] = screen;          // SCREEN VIEW

            HttpUtils.MakeHttpGet(GA_URL, collection2String(values));
        }

        private static NameValueCollection GetDefaultParams(string appName, string appVersion)
        {
            NameValueCollection values = new NameValueCollection();
            values["v"] = "1";                      // VERSION
            values["tid"] = TRACKING_ID;            // TRACKING ID
            values["cid"] = CID;                    // CLIENT ID

            values["an"] = appName;                 // APPLICATION NAME
            values["av"] = appVersion;              // APPLICATION VERSION

            return values;
        }

        private static string collection2String(NameValueCollection values)
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
