using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheInternetBuzz.Web.Controls;
using TheInternetBuzz.Data.Ads;

namespace TheInternetBuzz.Web.Pages.Ads
{
    public class Default : System.Web.UI.Page
    {
        public AdsTypeEnum AdsType
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["topic"];
            // url: /affiliates/Box250x250.html
            if ("Box250x250".Equals(query))
            {
                AdsType = AdsTypeEnum.Box250x250;
            }
            // url: /affiliates/Box160x600.html
            else if ("Box160x600".Equals(query))
            {
                AdsType = AdsTypeEnum.Box160x600;
            }
            // url: /affiliates/Box728x90.html
            else if ("Box728x90".Equals(query))
            {
                AdsType = AdsTypeEnum.Box728x90;
            }
            else
            {
                AdsType = AdsTypeEnum.Box728x90;
            }
        }
    }
}
