using System;
using System.Web;
using TheInternetBuzz.Connectors.JSON;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Topics;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Topics;

using TheInternetBuzz.Util;

using Newtonsoft.Json.Linq;

namespace TheInternetBuzz.Providers.Freebase
{
    public class FreebaseTopicService : IProviderTopicService
    {
        public FreebaseTopicService()
        {
        }

        public void FillTopic(TopicItem topicItem)
        {
            String query = topicItem.Query.ToLower().Replace(" ", "_");
            query = TextCleaner.CleanQuery(query);

            if (query.Length > 0)
            {
                try
                {
                    string urlTemplate = "http://www.freebase.com/experimental/topic/standard?id=/en/{0}";
                    string url = string.Format(urlTemplate, HttpUtility.UrlEncode(query));

                    JSONConnector JSONConnector = new JSONConnector();
                    JObject searchResultsJSONObject = JSONConnector.GetJSONObject(url);

                    new FreebaseTopicParser().Parse(searchResultsJSONObject, topicItem);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("FreebaseTopicService", "FillTopic", topicItem.ToString(), exception);
                }
            }    
        }

    }
}