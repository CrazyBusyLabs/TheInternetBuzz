using System;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Providers.Twitter.Data;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Providers.Twitter
{
    public class TwitterAuthParser 
    {
        public TwitterCredentials Parse(JObject searchResultsJSONObject)
        {
            TwitterCredentials credentials = null;
            try 
            {
                string accessToken = searchResultsJSONObject.Value<string>("access_token");
                credentials = new TwitterCredentials("Bearer", accessToken);
            }
            catch (Exception exception)
            {
                ErrorService.Log("TwitterAuthParser", "Auth", "Error Parsing", exception);
            }

            return credentials;
        }
    }
}
