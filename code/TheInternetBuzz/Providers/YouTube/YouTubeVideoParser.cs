using System;
using System.Collections;

using TheInternetBuzz.Web;
using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Data.Video;
using TheInternetBuzz.Data;

using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;

namespace TheInternetBuzz.Providers.YouTube
{
    public class YouTubeVideoParser
    {
        public void Parse(Feed<Video> videoFeed, VideoList videoList)
        {
            foreach (Video video in videoFeed.Entries)
            {
                string id = video.VideoId;
                string title = video.Title;

                if (video.Thumbnails != null && video.Thumbnails.Count > 0)
                {
                    foreach (MediaContent mediaContent in video.Contents)
                    {
                        if ("5".Equals(mediaContent.Format))
                        {
                            if ("120".Equals(video.Thumbnails[0].Width) && "90".Equals(video.Thumbnails[0].Height))
                            {
                                VideoItem videoItem = new VideoItem(id, title, ProviderEnum.YouTube);
                                videoItem.ThumbnailImageURL = video.Thumbnails[0].Url;
                                videoItem.ThumbnailImageWidth = video.Thumbnails[0].Width;
                                videoItem.ThumbnailImageHeight = video.Thumbnails[0].Height;

                                videoList.AddVideo(videoItem);
                            }
                        }
                    }
                }
            }
        }
    }
}