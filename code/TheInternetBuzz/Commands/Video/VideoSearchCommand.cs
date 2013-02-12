
using TheInternetBuzz.Services.Video;
using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Commands.Video
{
    public class VideoSearchCommand : ISearchVideoCommand
    {
        private string query = null;

        public VideoSearchCommand(string query)
        {
            this.query = query;
        }

        public VideoList GetVideo()
        {
            VideoList videoList  = new VideoService().Search(query);
            return videoList;
        }
    }
}