
using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data.Video;
using TheInternetBuzz.Services.Audit;
using TheInternetBuzz.Services.Categorization;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Video;

namespace TheInternetBuzz.Services.Trends
{
    public class TrendsService
    {
        private static readonly object Locker = new object();

        public TrendsService()
        {
        }

        public void BuildTrends()
        {
            AuditServiceItem auditServiceItem = AuditService.Register("TrendsService", "BuildTrends", "");
            AuditService.Start(auditServiceItem);

            TrendsBuilder.BuildTrendsData(null);

            AuditService.End(auditServiceItem);
        }

        public TrendsList GetTrends()
        {
            AuditServiceItem AuditServiceItem = AuditService.Register("TrendsService", "GetTrends", "");
            AuditService.Start(AuditServiceItem);

            TrendsList trendsList = TrendsCacheHelper.ReadTrends();
            if (trendsList == null)
            {
                lock (Locker)
                {
                    trendsList = TrendsCacheHelper.ReadTrends();
                    if (trendsList == null)
                    {
                        trendsList = TrendsSerializer.Deserialize();
                        if (trendsList != null) TrendsCacheHelper.CacheTrends(trendsList);
                    }              
                }
            }
            AuditService.End(AuditServiceItem);

            return trendsList;
        }

        public TrendsList GetYearlyTrends()
        {
            AuditServiceItem auditServiceItem = AuditService.Register("TrendsService", "GetYearlyTrends", "");
            AuditService.Start(auditServiceItem);

            TrendsList trendsList = new CategorizationService().GetTrends();

            AuditService.End(auditServiceItem);

            return trendsList;
        }

        public VideoList GetVideoTrends()
        {
            AuditServiceItem auditServiceItem = AuditService.Register("TrendsService", "GetVideoTrends", "");
            AuditService.Start(auditServiceItem);

            VideoList videoList = TrendsCacheHelper.ReadVideoTrends();
            if (videoList == null)
            {
                lock (Locker)
                {
                    videoList = TrendsCacheHelper.ReadVideoTrends();
                    if (videoList == null)
                    {
                        videoList = VideoListSerializer.Deserialize();
                        if (videoList != null) TrendsCacheHelper.CacheVideoTrends(videoList);
                    }
                }
            }

            AuditService.End(auditServiceItem);

            return videoList;
        }
    }
}
