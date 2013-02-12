using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheInternetBuzz.Services.Config;

namespace TheInternetBuzz.Web.Controls
{
    public class HtmlPostBodyControl : UserControl
    {
        public bool IncludeFacebookSDKScript
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
        }
    }
}