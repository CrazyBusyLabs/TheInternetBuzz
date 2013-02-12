using System;

using System.IO;
using System.Web;

using TheInternetBuzz.Services.Config;

namespace TheInternetBuzz.Web
{
    public static class URLBuilder
    {
        static private string baseURL = null;

        static public string BaseURL
        {
            get
            {
                if (baseURL == null)
                {
                    URLContext urlContext = URLContext.GetURLContext();

                    if (urlContext.Virtual == null)
                    {
                        baseURL = "/";
                    }
                    else
                    {
                        baseURL = "/" + urlContext.Virtual + "/";
                    }
                }
                return baseURL;
            }
        }

        static public string BuildURL(string section, string topic)
        {
            string url = null;

            if (ContainsSpecialCaracters(topic))
            {
                url = BaseURL + section + "/?topic=" + HttpUtility.UrlEncode(topic);
            }
            else
            {
                url = BaseURL + section + EncodeTopic("/" + topic);
            }
            return url;
        }

        static public string BuildFullLiveURL(string section, string topic)
        {
            string url = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_HOST_URL, "") + "/" + section;
            if (ContainsSpecialCaracters(topic))
            {
                url = url + "/?topic=" + HttpUtility.UrlEncode(topic);
            }
            else
            {
                url = url + EncodeTopic("/" + topic);
            }

            return url;
        }

        static public string BuildResourceURL(string uri)
        {
            return BaseURL + "resources/" + uri + "?" + TheInternetBuzz.Version.Major;
        }

        static public string BuildInternalURL(URLContext urlContext)
        {
            string url = null;

            if (urlContext.Virtual != null)
            {
                url = "/" + urlContext.Virtual;
            }
            if (urlContext.Resource != null)
            {
                url = url + urlContext.Resource;
            }
            else if (urlContext.Section == null)
            {
                url = url + "/pages/Default.aspx";
            }
            else
            {
                if (urlContext.Topic == null)
                {
                    url = url + "/pages/Default.aspx";
                }
                else
                {
                    bool fileExist = false;
                    if (!("topic".Equals(urlContext.Section)))
                    {
                        if (!ContainsSpecialCaracters(urlContext.Topic))
                        {
                            string relativePath = "~/pages/" + urlContext.Section + "/" + urlContext.Topic + ".aspx";
                            string filepath = System.Web.Hosting.HostingEnvironment.MapPath(relativePath);
                            if (File.Exists(filepath))
                            {
                                url = url + "/pages/" + urlContext.Section + "/" + urlContext.Topic + ".aspx";
                                fileExist = true;
                            }
                        }
                    }
                    if (!fileExist)
                    {
                        url = url + "/pages/" + urlContext.Section + "/Default.aspx?topic=" + HttpUtility.UrlEncode(urlContext.Topic);
                        if (urlContext.Page != null)
                        {
                            url = url + "&page=" + urlContext.Page; ;
                        }
                    }
                }
            }

            return url;
        }

        static private string EncodeTopic(string topic)
        {
            return HttpUtility.UrlPathEncode(topic.Replace(" ", "-"));
        }

        static private bool ContainsSpecialCaracters(string topic)
        {
            return (topic.Contains("\"") || topic.Contains("+")
                        || topic.Contains("%") || topic.Contains("|")
                        || topic.Contains(":") || topic.Contains("&")
                        || topic.Contains("/") || topic.Contains("-"));
        }
    }
}
