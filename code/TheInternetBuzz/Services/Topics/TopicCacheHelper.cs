using System;

using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Topics;

namespace TheInternetBuzz.Services.Topics
{
    public static class TopicCacheHelper
    {
        public static void CacheTopicItem(string query, TopicItem topicItem)
        {
            string cacheName = "topic-" + query;
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_TOPIC_EXPIRATION, 60);
            CacheService.Put(cacheName, cacheExpiration, topicItem);
        }

        public static TopicItem ReadTopicItem(string query)
        {
            string cacheName = "topic-" + query;
            return (TopicItem)CacheService.Read(cacheName);
        }
    }
}
