using System;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Providers.Twitter
{
    public class TwitterSearchParser 
    {
        public void Parse(JObject searchResultsJSONObject, SearchResultList searchResultList)
        {
            try 
            {
                JArray resultsJArray = searchResultsJSONObject.Value<JArray>("results");
                foreach (JObject resultJObject in resultsJArray)
                {
                    SearchResultItem resultItem = new SearchResultItem();
                    resultItem.Provider = ProviderEnum.Twitter;

                    string content = resultJObject.Value<string>("text");
                    resultItem.Abstract = content.ParseTwitterURL().ParseTwitterUsername().ParseTwitterHashtag();

                    string image = resultJObject.Value<string>("profile_image_url");
                    resultItem.ImageURL = image;

                    string userid = resultJObject.Value<string>("from_user");
                    resultItem.Title = userid;

                    string url = "http://www.twitter.com/" + userid;
                    resultItem.URL = url;

                    string publishedDateValue = resultJObject.Value<string>("created_at");
                    if (publishedDateValue != null)
                    {
                        // format: Sun, 30 Oct 2011 19:54:11 +0000",
                        publishedDateValue = publishedDateValue.Replace("+0000", "+0");
                        DateTime publishedDate = DateParser.Parse(publishedDateValue, "ddd, dd MMM yyyy HH':'mm':'ss z");
                        resultItem.PublishedDate = publishedDate;
                    }

                    searchResultList.Add(resultItem);
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("TwitterSearchParser", "Search", searchResultList.ToString(), exception);
            }
        }
    }
}
