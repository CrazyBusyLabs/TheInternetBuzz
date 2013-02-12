
using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Services.Trends
{
    public static class TrendsCacheHelper
    {
        public static void CacheTrends(TrendsList trendsList)
        {
            int cacheExpiration = int.MaxValue;
            CacheService.Put("trends", cacheExpiration, trendsList);
        }

        public static TrendsList ReadTrends()
        {
            return (TrendsList)CacheService.Read("trends");
        }

        public static void CacheVideoTrends(VideoList videoList)
        {
            int cacheExpiration = int.MaxValue;
            CacheService.Put("video-trends", cacheExpiration, videoList);
        }

        public static VideoList ReadVideoTrends()
        {
            return (VideoList)CacheService.Read("video-trends");
        }
    }
}
