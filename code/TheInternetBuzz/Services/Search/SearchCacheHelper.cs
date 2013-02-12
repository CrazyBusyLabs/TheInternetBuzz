using System;

using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Search;

namespace TheInternetBuzz.Services.Search
{
    public static class SearchCacheHelper
    {
        public static void CacheSearchResultList(SearchTypeEnum searchType, string query, int page, SearchResultList searchResultList)
        {
            string cacheName = "search-" + searchType + "-" + query + "-" + page;
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_EXPIRATION, 60);
            CacheService.Put(cacheName, cacheExpiration, searchResultList);
        }

        public static SearchResultList ReadSearchResultList(SearchTypeEnum searchType, string query, int page)
        {
            string cacheName = "search-" + searchType + "-" + query + "-" + page;
            return (SearchResultList) CacheService.Read(cacheName);
        }
    }
}
