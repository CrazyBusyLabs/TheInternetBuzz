
using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Explanation;

namespace TheInternetBuzz.Services.Explanation
{
    public static class ExplanationCacheHelper
    {
        public static void CacheExplanationItem(string query, ExplanationItem explanationItem)
        {
            string cacheName = "explanation-" + query;
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_EXPLANATION_EXPIRATION, 600);
            CacheService.Put(cacheName, cacheExpiration, explanationItem);
        }

        public static ExplanationItem ReadExplanationItem(string query)
        {
            string cacheName = "explanation-" + query;
            return (ExplanationItem)CacheService.Read(cacheName);
        }
    }
}
