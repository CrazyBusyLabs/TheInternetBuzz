using System;
using System.Web;
using System.Web.UI;

using TheInternetBuzz.Web;
using TheInternetBuzz.Commands.Video;
using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Web.Controls
{
    public partial class VideoControl : AsyncUserControl
    {
        public ISearchVideoCommand VideoCommand
        {
            get;
            set;
        }

        public VideoList VideoList
        {
            get;
            set;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected override void LoadData()
        {
            Trace.Write("VideoTrendsControl LoadData() - Start Calling Video Command");
            if (VideoCommand != null)
            {
                VideoList = VideoCommand.GetVideo();
            }
            Trace.Write("VideoTrendsControl LoadData() - End Calling Video Command");
        }

        protected override bool isDisplayControl()
        {
            return VideoList != null && VideoList.Count() > 0;
        }

        protected void DisplayTrends()
        {
            if (VideoList != null)
            {
                foreach (VideoItem videoItem in VideoList)
                {
                    string id = videoItem.ID;
                    string title = HttpUtility.HtmlEncode(videoItem.Title);
                    string thumbnailUrl = videoItem.ThumbnailImageURL;

                    Response.Write("<li><a href=\"http://www.youtube.com/watch?v=" + id + "\" title=\"" + title + "\" onclick=\"trackEvent('video','view','" + id + "'); return true;\" class=\"videoTrendsLink\"><img src=\"" + videoItem.ThumbnailImageURL + "\" width=\"" + videoItem.ThumbnailImageWidth + "\" height=\"" + videoItem.ThumbnailImageHeight + "\" alt=\"\" class=\"videoTrendsThumbnail\"/></a></li>");
                }
            }
        }
    }
}

