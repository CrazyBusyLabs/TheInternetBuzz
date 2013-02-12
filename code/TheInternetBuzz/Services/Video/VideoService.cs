using System;

using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Data.Video;
using TheInternetBuzz.Services.Audit;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Providers.YouTube;

namespace TheInternetBuzz.Services.Video
{
    public class VideoService
    {
        public VideoService()
        {
        }

        public VideoList Search(String query)
        {
            AuditServiceItem auditServiceItem = AuditService.Register("VideoSearchService", "Search", query);
            AuditService.Start(auditServiceItem);

            VideoList videoList = VideoSearchCacheHelper.ReadSearchResultList(query);

            if (videoList == null)
            {
                try
                {
                    IProviderVideoSearchService youTubeSearchService = new YouTubeSearchService();
                    videoList = youTubeSearchService.Search(query);

                    VideoSearchCacheHelper.CacheSearchResultList(query, videoList);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("VideoService", "Search", query, exception);
                }
            }

            AuditService.End(auditServiceItem);

            return videoList;
        }
    }
}
