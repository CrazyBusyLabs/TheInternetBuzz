using System;
using System.Web;
using System.Collections.Generic;

using TheInternetBuzz.Services.Logging;

namespace TheInternetBuzz.Web.Modules
{
    public class SecurityModule : IHttpModule
    {
        private List<string> headers;

        public SecurityModule()        
        {
            this.headers = new List<string>
                {
                    "Server"
                };        
        }

        public void Init(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += new EventHandler(this.BeginRequest);
            httpApplication.PreSendRequestHeaders += this.OnPreSendRequestHeaders;
        }

        public void Dispose()
        {
        }

        private void BeginRequest(object sender, EventArgs eventArgs)
        {
            HttpApplication httpApplication = (HttpApplication)sender;
            HttpContext context = (HttpContext)httpApplication.Context;
            HttpRequest httpRequest = context.Request;
            HttpResponse httpResponse = context.Response;

            string filepath = httpRequest.FilePath;
            string raw = httpRequest.RawUrl;
            string path = httpRequest.Path;

            if (raw.Contains(" ") || raw.Contains("??") || raw.Contains("..") || raw.EndsWith("-"))
            {
                LogService.Warn(typeof(SecurityModule), "The URL " + raw  + " triggered a 400");

                httpResponse.StatusCode = 400;
                httpResponse.Status = "400 Bad Request";
                httpResponse.End();

            }
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e) 
        {
            this.headers.ForEach(h => HttpContext.Current.Response.Headers.Remove(h));
        }
    }


}
