using System;
using System.Net;
using System.IO;

using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Logging;

namespace TheInternetBuzz.Connectors.HTTP
{
    public class HTTPConnector
    {
        public string GetResponse(string url, string mime)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = mime;
            request.UserAgent = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_USER_AGENT, "");
            request.Referer = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_REFERER, "");

            string response = null;
            HttpStatusCode status;

            using (HttpWebResponse webResponse = (HttpWebResponse) request.GetResponse())
            {
                status = webResponse.StatusCode;
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                    reader.Close();
                    webResponse.Close();
                }
            }
            LogService.Debug(LoggerKeys.THEINTERNETBUZZ_LOGGING_CONNECTOR, url + ": [" + status + "] " + response);

            return response;
        }

        public HttpStatusCode GetStatus(string url, string mime)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = mime;
            request.UserAgent = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_USER_AGENT, "");
            request.Referer = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_REFERER, "");

            HttpStatusCode status = HttpStatusCode.NotFound;

            try
            {
                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    status = webResponse.StatusCode;
                }
            }
            catch (Exception e)
            {
                LogService.Error(LoggerKeys.THEINTERNETBUZZ_LOGGING_CONNECTOR, url,  e);
            }
            LogService.Debug(LoggerKeys.THEINTERNETBUZZ_LOGGING_CONNECTOR, url + ": [" + status + "] ");

            return status;
        }

        public HttpWebRequest BuildWebRequest(string url, string mime)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = mime;
            request.UserAgent = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_USER_AGENT, "");
            request.Referer = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_REFERER, "");

            return request;
        }
    }
}