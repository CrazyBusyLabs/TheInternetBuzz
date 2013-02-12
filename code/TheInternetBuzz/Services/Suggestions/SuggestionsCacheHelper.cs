using System;

using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Suggestions;

namespace TheInternetBuzz.Services.Suggestions
{
    public static class SuggestionsCacheHelper
    {
        public static void CacheSuggestionList(string query, SuggestionsList suggestionsList)
        {
            string cacheName = "suggestion-" + query;
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SUGGESTIONS_EXPIRATION, 60);
            CacheService.Put(cacheName, cacheExpiration, suggestionsList);
        }

        public static SuggestionsList ReadSuggestionsList(string query)
        {
            string cacheName = "suggestion-" + query;
            return (SuggestionsList)CacheService.Read(cacheName);
        }
    }
}
