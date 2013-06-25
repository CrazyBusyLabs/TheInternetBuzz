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
                    string urlTemplate = "https://www.googleapis.com/freebase/v1/topic/en/{0}?key={1}&{2}";
                    string apiKey = ConfigService.GetConfig(ConfigKeys.GOOGLE_API_KEY, "");
                    string[] domains = new string[] { 
                                         "/common/topic/alias", 
                                         "/common/topic/description", 
                                         "/common/topic/image",
                                         "/common/topic/official_website",
                                         "/common/topic/social_media_presence",
                                         "/influence/influence_node/influenced_by", 
                                         "/people/person/date_of_birth"
                    };
                    string filters = "filter=" + String.Join("&filter=", domains);

                    string url = string.Format(urlTemplate, HttpUtility.UrlEncode(query),apiKey,filters);

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