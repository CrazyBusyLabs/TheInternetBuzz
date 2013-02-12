using System;

using TheInternetBuzz.Commands.Trends;
using TheInternetBuzz.Commands.Video;


namespace TheInternetBuzz.Web.Pages
{
    public class Default : System.Web.UI.Page
    {
        protected global::TheInternetBuzz.Web.Controls.CloudControl cloudHtml;
        protected global::TheInternetBuzz.Web.Controls.TitleControl titleHtml;
        protected global::TheInternetBuzz.Web.Controls.VideoControl videoHtml;
        protected global::TheInternetBuzz.Web.Controls.TilesControl tilesHtml;

        protected void Page_Load(object sender, EventArgs e)
        {
            titleHtml.Title = "What&#8217;s buzzing on the internet?";

            cloudHtml.TrendsCommand = new TrendsCommand();
            videoHtml.VideoCommand = new VideoTrendsCommand();
            tilesHtml.TrendsCommand = new TrendsCommand();
        }
    }
}
