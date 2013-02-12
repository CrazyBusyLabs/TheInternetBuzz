using System;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Providers.Freebase
{
    public class FreebaseTopicParser 
    {
        public void Parse(JObject topicJSONObject, TopicItem topicItem)
        {
            JToken topicToken = topicJSONObject.First.First;
            if (topicToken != null)
            {
                JObject resultDataJObject = topicToken.Value<JObject>("result");
                if (resultDataJObject != null)
                {
                    String description = resultDataJObject.Value<string>("description");
                    topicItem.FreebaseSummary = description;

                    String freebaseURL = resultDataJObject.Value<string>("url");
                    topicItem.FreebaseURL = freebaseURL;

                    String freebaseThumbnailURL = resultDataJObject.Value<string>("thumbnail");
                    topicItem.FreebaseThumbnailURL = freebaseThumbnailURL;

                    JArray webpageJArray = resultDataJObject.Value<JArray>("webpage");
                    foreach (JObject webpageJObject in webpageJArray)
                    {
                        string text = webpageJObject.Value<string>("text");
                        string url = webpageJObject.Value<string>("url");

                        if (text != null)
                        {
                            if ("Wikipedia".Equals(text))
                            {
                                topicItem.WikipediaURL = url;
                            }
                            else if ("Twitter Page".Equals(text))
                            {
                                topicItem.TwitterURL = url;
                            }
                            else if ("MySpace Page".Equals(text))
                            {
                                topicItem.MySpaceURL = url;
                            }
                            else if ("Facebook Page".Equals(text))
                            {
                                topicItem.FacebookURL = url;
                            }
                            else if ("Official Website".Equals(text))
                            {
                                topicItem.WebsiteURL = url;
                            }
                        }
                    }
                }
            }
        }
    }
}
