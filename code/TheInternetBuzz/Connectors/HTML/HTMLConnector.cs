using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using TheInternetBuzz.Connectors.HTTP;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;

namespace TheInternetBuzz.Connectors.HTML
{
    public class HTMLConnector 
    {
        public string GetHTMLDocument(string path)
        {
            string htmlDocument = null;

            try
            {
                if (path.StartsWith("http"))
                {
                    htmlDocument = new HTTPConnector().GetResponse(path, "text/html");
                }
                else
                {
                    LogService.Warn(typeof(HTMLConnector), "Reading from a file not supported");
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("HTMLConnector", "GetHTMLDocument", path, exception);
            }

            return htmlDocument;
        }
    }
}