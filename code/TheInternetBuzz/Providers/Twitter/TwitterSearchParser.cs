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
                JArray resultsJArray = searchResultsJSONObject.Value<JArray>("statuses");
                foreach (JObject resultJObject in resultsJArray)
                {
                    SearchResultItem resultItem = new SearchResultItem();
                    resultItem.Provider = ProviderEnum.Twitter;

                    string content = resultJObject.Value<string>("text");

                    if (content != null && !content.Contains("#"))
                    {
                        resultItem.Abstract = content.ParseTwitterURL().ParseTwitterUsername().ParseTwitterHashtag();

                        JObject userDataJObject = resultJObject.Value<JObject>("user");

                        string image = userDataJObject.Value<string>("profile_image_url");
                        resultItem.ImageURL = image;

                        string name = userDataJObject.Value<string>("name");
                        resultItem.Title = name;

                        string screenName = userDataJObject.Value<string>("screen_name");
                        string url = "http://www.twitter.com/" + screenName;
                        resultItem.URL = url;

                        string publishedDateValue = resultJObject.Value<string>("created_at");
                        if (publishedDateValue != null)
                        {
                            // format: Sun, 30 Oct 2011 19:54:11 +0000",(publishedDateValue, "ddd, dd MMM yyyy HH':'mm':'ss z");
                            // new format Mon Sep 24 03:35:21 +0000 2012
                            //publishedDateValue = publishedDateValue.Replace("+0000", "+0");
                            //DateTime publishedDate = DateParser.Parse(publishedDateValue, "ddd MMM HH':'mm':'ss z yyyy");
                            //resultItem.PublishedDate = publishedDate;
                        }

                        searchResultList.Add(resultItem);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("TwitterSearchParser", "Search", searchResultList.ToString(), exception);
            }
        }
    }
}
