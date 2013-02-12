
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Commands.Video
{
    public class VideoTrendsCommand : ISearchVideoCommand
    {
        public VideoList GetVideo()
        {
            return new TrendsService().GetVideoTrends();
        }
    }
}