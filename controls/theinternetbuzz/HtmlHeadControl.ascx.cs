using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheInternetBuzz.Services.Config;

namespace TheInternetBuzz.Web.Controls
{
    public class HtmlHeadControl : UserControl
    {
        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Keywords
        {
            get;
            set;
        }

        public bool IncludeJQueryScript
        {
            get;
            set;
        }

        public bool IncludeIsotope
        {
            get;
            set;
        }

        public bool IncludePrettyPhotoScript
        {
            get;
            set;
        }

        public bool IncludeSliderScript
        {
            get;
            set;
        }

        public bool IncludeGooglePlusOneScript
        {
            get;
            set;
        }

        public bool IncludeUserVoice
        {
            get;
            set;
        }

        public bool RobotIndexing
        {
            get;
            set;
        }

        protected string GoogleAnalyticsAccount
        {
            get;
            set;
        }

        protected string FacebookAPIKey
        {
            get;
            set;

        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            FacebookAPIKey = ConfigService.GetConfig(ConfigKeys.FACEBOOK_API_KEY, "");
            GoogleAnalyticsAccount = ConfigService.GetConfig(ConfigKeys.GOOGLE_ANALYTICS_ACCOUNT, "");
        }
    }
}