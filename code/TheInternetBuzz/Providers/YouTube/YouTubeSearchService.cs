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
    public class YouTubeSearchService : IProviderVideoSearchService
    {
        public VideoList Search(string query)
        {
            VideoList videoList = new VideoList();
            try
            {
                string api = ConfigService.GetConfig(ConfigKeys.YOUTUBE_API_KEY, "");
                YouTubeRequestSettings settings = new YouTubeRequestSettings("TheInternetBuzz.org", "TheInternetBuzz.org", api);
                YouTubeRequest request = new YouTubeRequest(settings);

                AddVideo(request, ConfigKeys.YOUTUBE_SEARCH_MAXRESULTS, query, videoList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("YouTubeSearchService", "Search", query, exception);
            }

            return videoList;
        }

        private void AddVideo(YouTubeRequest request, string maxResultsKey, string query, VideoList videoList)
        {
            int maxResults = ConfigService.GetConfig(maxResultsKey, 0);
            if (maxResults > 0)
            {
                YouTubeQuery youtubeQuery = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
                youtubeQuery.Query = "%22" + query +  "%22";
                youtubeQuery.SafeSearch = YouTubeQuery.SafeSearchValues.Strict;
                youtubeQuery.NumberToRetrieve = maxResults;
                Feed<Video> videos = request.Get<Video>(youtubeQuery);

                YouTubeVideoParser parser = new YouTubeVideoParser();
                parser.Parse(videos, videoList);
            }
        }

    } 
}
