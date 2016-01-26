using System;
using System.Web;
using System.Text.RegularExpressions;

using TheInternetBuzz.Services.Config;

namespace TheInternetBuzz.Web.Modules
{
    public class URLRewriteModule : IHttpModule
    {
        public URLRewriteModule()
        {
        }

        public void Init(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += new EventHandler(this.BeginRequest);
        }

        public void Dispose()
        {
        }

        private void BeginRequest(object sender, EventArgs eventArgs)
        {
            HttpApplication httpApplication = (HttpApplication) sender;
            HttpContext context = (HttpContext) httpApplication.Context;
            HttpRequest httpRequest = context.Request;
            HttpResponse httpResponse = context.Response;

            string filepath = httpRequest.FilePath;

            if (filepath.Contains("/pages"))
            {
                // No special Processing
            }
            else if (filepath.Contains("/Trace.axd") || filepath.Contains("/CacheManager.axd"))
            {
                // No special Processing
            }
            else if (filepath.Contains("/resources"))
            {
                // No special Processing
            }
            else
            {
                // Rewrite the URL
                URLContext urlContext = new URLContext();
                URLContext.StoreURLContext(urlContext);
                urlContext.Scheme = httpRequest.Url.Scheme;
                urlContext.Host = httpRequest.Url.Host;
                ProcessParameters(urlContext, httpRequest);
                ProcessFilePath(urlContext, filepath);

                string newPath = URLBuilder.BuildInternalURL(urlContext);
                context.RewritePath(newPath, false);
            }
        }

        private void ProcessParameters(URLContext URLContext, HttpRequest httpRequest)
        {
            if (httpRequest.QueryString["topic"] != null && httpRequest.QueryString["topic"].Length > 0)
            {
                URLContext.Topic = httpRequest.QueryString["topic"];
            }

            if (httpRequest.QueryString["page"] != null && httpRequest.QueryString["page"].Length > 0)
            {
                URLContext.Page = httpRequest.QueryString["page"];
            }
        }

        private URLContext ProcessFilePath(URLContext urlContext, string filepath)
        {
            URLPathTokenizer tokenizer = new URLPathTokenizer(filepath);

            string token = tokenizer.nextToken();
            if (token == null) return urlContext;

            // parse virtual
            if ("TheInternetBuzzWebApplication".Equals(token))
            {
                urlContext.Virtual = token;
                token = tokenizer.nextToken();
                if (token == null) return urlContext;
            }

            // parse section
            if (IsValidSection(token))
            {
                urlContext.Section = token;
                token = tokenizer.nextToken();
                if (token == null) return urlContext;

                // parse topic
                urlContext.Topic = token.Replace("-"," ");
                token = tokenizer.nextToken();
                if (token == null) return urlContext;

                // parse page
                urlContext.Page = token;
            }

            // resources
            else
            {
                if ("sitemap.xml".Equals(token))
                {
                    urlContext.Resource = "/resources/Sitemap.aspx";
                }
                else
                {
                    urlContext.Resource = "/resources/" + token;
                }
            }



            return urlContext;
        }

        private bool IsValidSection(string token)
        {
            return "admin".Equals(token) || "affiliates".Equals(token) || 
                "theinternetbuzz".Equals(token) || "topic".Equals(token); ;
        }

    }
}