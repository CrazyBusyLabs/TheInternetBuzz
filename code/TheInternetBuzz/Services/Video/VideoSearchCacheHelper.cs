
using TheInternetBuzz.Services.Cache;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Services.Video
{
    public static class VideoSearchCacheHelper
    {
        public static void CacheSearchResultList(string query, VideoList videoList)
        {
            string cacheName = "video-" + query;
            int cacheExpiration = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_VIDEO_SEARCH_EXPIRATION, 60);
            CacheService.Put(cacheName, cacheExpiration, videoList);
        }

        public static VideoList ReadSearchResultList(string query)
        {
            string cacheName = "video-" + query;
            return (VideoList) CacheService.Read(cacheName);
        }
    }
}
