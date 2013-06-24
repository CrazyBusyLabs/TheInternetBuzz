using System;
using System.Web;

using TheInternetBuzz.Connectors.JSON;
using TheInternetBuzz.Providers.Twitter.Data;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Util;

using Newtonsoft.Json.Linq;
using System.Net;

namespace TheInternetBuzz.Providers.Twitter
{
    public class TwitterAuthService
    {
        static TwitterCredentials twitterCredentials = null;

        public TwitterAuthService()
        {
        }

        public TwitterCredentials Authenticate()
        {
            if (twitterCredentials == null)
            {
                try
                {
                    string url = "https://api.twitter.com/oauth2/token";

                    string consumerSecret = ConfigService.GetConfig(ConfigKeys.TWITTER_ACCESS_CONSUMER_SECRET, "");
                    string consumerKey = ConfigService.GetConfig(ConfigKeys.TWITTER_ACCESS_CONSUMER_KEY, "");
                    String bearerCredentials = HttpUtility.UrlEncode(consumerKey) + ":" + HttpUtility.UrlEncode(consumerSecret);
                    bearerCredentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(bearerCredentials));

                    JSONConnector JSONConnector = new JSONConnector();
                    JObject searchResultsJSONObject = JSONConnector.PostJSONObjectWithHeader(url, "Authorization", "Basic " + bearerCredentials, "grant_type=client_credentials");

                    twitterCredentials = new TwitterAuthParser().Parse(searchResultsJSONObject);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("TwitterSearchService", "Auth", "Authentication Failed", exception);
                }
            }

            return twitterCredentials;
        }
    } 
}
