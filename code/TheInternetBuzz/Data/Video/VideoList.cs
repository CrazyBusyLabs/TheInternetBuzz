using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Video
{
    public class VideoList : List 
    {
        public void AddVideo(VideoItem newVideo)
        {
            VideoItem video = GetVideo(newVideo.ID);
            if (video == null)
            {
                Add(newVideo);
            }
        }

        public VideoItem GetVideo(string ID)
        {
            VideoItem foundVideoItem = null;

            foreach (VideoItem videoItem in this)
            {
                if (videoItem.ID.Equals(ID, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundVideoItem = videoItem;
                    break;
                }
            }

            return foundVideoItem;
        }
    }
}
