using System;

using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Video;
using TheInternetBuzz.Data.Video;

using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;

namespace TheInternetBuzz.Providers.YouTube
{
    public class YouTubeTrendsService
    {
        private static string RecentlyFeaturedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/recently_featured?start-index=1&max-results={0}";
        private static string TopRatedFeedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/top_rated?time=this_week&start-index=1&max-results={0}&format=5&safeSearch=strict&v=2";
        private static string TopFavouritesFeedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/top_favorites?time=this_week&start-index=1&max-results={0}&format=5&safeSearch=strict&v=2";
        private static string MostViewedFeedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/most_viewed?time=this_week&start-index=1&max-results={0}&format=5&safeSearch=strict&v=2";
        private static string MostPopularFeedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/most_popular?time=this_week&start-index=1&max-results={0}&format=5&safeSearch=strict&v=2";
        private static string MostDiscussedFeedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/most_discussed?time=this_week&start-index=1&max-results={0}&format=5&safeSearch=strict&v=2";
        private static string MostRespondedFeedUrlTemplate = "http://gdata.youtube.com/feeds/api/standardfeeds/most_responded?time=this_week&start-index=1&max-results={0}&format=5&safeSearch=strict&v=2";

        public VideoList GetVideo()
        {
            VideoList videoList = new VideoList();
            try
            {
                string api = ConfigService.GetConfig(ConfigKeys.YOUTUBE_API_KEY, "");
                YouTubeRequestSettings settings = new YouTubeRequestSettings("TheInternetBuzz.com", "TheInternetBuzz.com", api);
                YouTubeRequest request = new YouTubeRequest(settings);

                AddVideo(request, RecentlyFeaturedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_RECENTLYFEATURED_MAXRESULTS, videoList);
                AddVideo(request, TopRatedFeedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_TOPRATED_MAXRESULTS, videoList);
                AddVideo(request, TopFavouritesFeedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_TOPFAVOURITES_MAXRESULTS, videoList);
                AddVideo(request, MostViewedFeedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_MOSTVIEWED_MAXRESULTS, videoList);
                AddVideo(request, MostPopularFeedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_MOSTPOPULAR_MAXRESULTS, videoList);
                AddVideo(request, MostDiscussedFeedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_MOSTDISCUSSED_MAXRESULTS, videoList);
                AddVideo(request, MostRespondedFeedUrlTemplate, ConfigKeys.YOUTUBE_FEEDS_MOSTRESPONDED_MAXRESULTS, videoList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("YouTubeTrendsService", "GetVideo", null, exception);
            }

            return videoList;
        }

        private void AddVideo(YouTubeRequest request, string urlTemplate, string maxResultsKey, VideoList videoList)
        {
            int maxResults = ConfigService.GetConfig(maxResultsKey, 0);
            if (maxResults > 0)
            {
                string url = string.Format(urlTemplate, maxResults);
                Feed<Video> videos = request.Get<Video>(new Uri(url));

                YouTubeVideoParser parser = new YouTubeVideoParser();
                parser.Parse(videos, videoList);
            }
        }

    } 
}
