using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

using TheInternetBuzz.Data.Event;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Connectors.HTTP;

namespace TheInternetBuzz.Providers.Splunk
{
    public class SplunkService
    {
        static private string SPLUNK_URL_TEMPLATE = "https://api.splunkstorm.com/1/inputs/http?project={0}&host={1}&source={2}&sourcetype={3}";

        static private string ACCESS_TOKEN = ConfigService.GetConfig(ConfigKeys.SPLUNK_ACCESS_TOKEN, "");
        static private string PROJECT_ID = ConfigService.GetConfig(ConfigKeys.SPLUNK_PROJECT_ID, "");
        static private string HOST = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_HOST, "");

        public static void Log(EventItem eventItem)
        {
            ThreadPool.QueueUserWorkItem(callback => Send(eventItem));
        }

        public static void Send(EventItem eventItem)
        {
            string url = string.Format(SPLUNK_URL_TEMPLATE,
                PROJECT_ID, HOST, eventItem.Source.ToString("f"), eventItem.SourceType.ToString("f"));

            HTTPConnector HTTPConnector = new HTTPConnector();
            string response = HTTPConnector.PostDataWithCredentials(url, "x", ACCESS_TOKEN, eventItem.Data);

        }

    }
}