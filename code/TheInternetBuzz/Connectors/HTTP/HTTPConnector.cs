using System;
using System.Net;
using System.IO;
using System.Text;


using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Logging;
using System.Web;

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

        public string GetResponseWithHeader(string url, string headerName, string headerValue, string mime)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = mime;
            request.UserAgent = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_USER_AGENT, "");
            request.Referer = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_REFERER, "");
            request.Headers.Add(headerName, headerValue);

            string response = null;
            HttpStatusCode status;

            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
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
  
        public string PostDataWithCredentials(string url, string user, string password, string data)
        {
            string response = null;

            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = BuildWebRequest(url, null);

                // Authenitcation
                NetworkCredential networkCredential = new NetworkCredential(user, password);
                CredentialCache crendentialCache = new CredentialCache();
                crendentialCache.Add(url, 443, "Basic", networkCredential);
                request.Credentials = networkCredential;
                request.PreAuthenticate = true;

                // Set the Method property of the request to POST.
                request.Method = "POST";
            
                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(data);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
            
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
            
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
            
                dataStream.Close();
            
                // Get the response.
                WebResponse webResponse = request.GetResponse();
            
                // Get the stream containing content returned by the server.
                dataStream = webResponse.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
            
                // Read the content.
                response = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                webResponse.Close();
            }
            catch (Exception e)
            {
                LogService.Error(LoggerKeys.THEINTERNETBUZZ_LOGGING_CONNECTOR, url, e);
            }
            return response;
        }

        public string PostDataWithHeader(string url, string headerName, string headerValue, string data)
        {
            string response = null;

            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = BuildWebRequest(url, null);
                request.Headers.Add(headerName, headerValue);

                // Set the Method property of the request to POST.
                request.Method = "POST";

                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(data);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();

                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.

                dataStream.Close();

                // Get the response.
                WebResponse webResponse = request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = webResponse.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);

                // Read the content.
                response = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                webResponse.Close();
            }
            catch (Exception e)
            {
                LogService.Error(LoggerKeys.THEINTERNETBUZZ_LOGGING_CONNECTOR, url, e);
            }
            return response;
        }


        public HttpWebRequest BuildWebRequest(string url, string mime)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (mime != null) request.Accept = mime;
            request.UserAgent = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_USER_AGENT, "");
            request.Referer = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CONNECTOR_REFERER, "");

            return request;
        }
    }
}