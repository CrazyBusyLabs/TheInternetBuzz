using System;

using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Categorization;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Services.Categorization
{
    public static class CategorizationCacheHelper
    {
        public static void CacheCategoriesList(CategoriesList categoriesList)
        {
            string cacheName = "categories";
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CATEGORIES_EXPIRATION, 60);
            CacheService.Put(cacheName, cacheExpiration, categoriesList);
        }

        public static CategoriesList ReadCategoriesList()
        {
            string cacheName = "categories";
            return (CategoriesList)CacheService.Read(cacheName);
        }

        public static void CacheTrends(TrendsList trendsList)
        {
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_CATEGORIES_EXPIRATION, 60);
            CacheService.Put("yearly-trends", cacheExpiration, trendsList);
        }

        public static TrendsList ReadTrends()
        {
            return (TrendsList)CacheService.Read("yearly-trends");
        }
    }
}
