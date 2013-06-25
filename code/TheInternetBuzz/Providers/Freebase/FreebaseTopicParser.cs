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
            JObject propertyJObject = topicJSONObject.Value<JObject>("property");

            if (propertyJObject != null) 
            {
                foreach (JToken child in propertyJObject.Children())
                {
                    var property = child as JProperty;
                    if (property != null && property.Name != null)
                    {
                        switch (property.Name)
                        {
                            case "/common/topic/description":
                                ParseTopicDescription((JObject)property.Value, topicItem);
                                break;
                            case "/common/topic/image":
                                ParseTopicImage((JObject)property.Value, topicItem);
                                break;
                            case "/common/topic/official_website":
                                ParseTopicOfficialWebsite((JObject)property.Value, topicItem);
                                break;
                            case "/common/topic/social_media_presence":
                                ParseTopicSocialMediaPresence((JObject)property.Value, topicItem);
                                break;
                            case "/common/topic/alias":
                                ParseTopicAlias((JObject)property.Value, topicItem);
                                break;
                        }
                    }
                }
            }
        }

        private void ParseTopicDescription(JObject descriptionJSONObject, TopicItem topicItem)
        {
            JArray valuesJArray = descriptionJSONObject.Value<JArray>("values");
            if (valuesJArray != null && valuesJArray.Count > 0)
            {
                string description = valuesJArray[0].Value<string>("value");
                topicItem.FreebaseSummary = description;
            }
        }

        private void ParseTopicImage(JObject descriptionJSONObject, TopicItem topicItem)
        {
            JArray valuesJArray = descriptionJSONObject.Value<JArray>("values");
            if (valuesJArray != null && valuesJArray.Count > 0)
            {
                string id = valuesJArray[0].Value<string>("id");
                topicItem.FreebaseImageURL = "https://usercontent.googleapis.com/freebase/v1/image" + id;
            }
        }

        private void ParseTopicOfficialWebsite(JObject descriptionJSONObject, TopicItem topicItem)
        {
            JArray valuesJArray = descriptionJSONObject.Value<JArray>("values");
            if (valuesJArray != null && valuesJArray.Count > 0)
            {
                string description = valuesJArray[0].Value<string>("value");
                topicItem.WebsiteURL = description;
            }
        }

        private void ParseTopicSocialMediaPresence(JObject descriptionJSONObject, TopicItem topicItem)
        {
            JArray valuesJArray = descriptionJSONObject.Value<JArray>("values");
            if (valuesJArray != null)
            {
                foreach (JObject valueJObject in valuesJArray)
                {
                    string value = valueJObject.Value<string>("value");
                    if (value != null)
                    {
                        if (value.Contains("twitter")) topicItem.TwitterURL = value;
                        else if (value.Contains("facebook")) topicItem.FacebookURL = value;
                        else if (value.Contains("myspace")) topicItem.MySpaceURL = value;
                    }
                }
            }
        }
        private void ParseTopicAlias(JObject descriptionJSONObject, TopicItem topicItem)
        {
            JArray valuesJArray = descriptionJSONObject.Value<JArray>("values");
            if (valuesJArray != null)
            {
                topicItem.FreebaseAliases = new string[valuesJArray.Count];
                int i = 0;
                foreach (JObject valueJObject in valuesJArray)
                {
                    string value = valueJObject.Value<string>("value");
                    topicItem.FreebaseAliases[i] = value;
                    i++;
                }
            }
        }
    }
}








