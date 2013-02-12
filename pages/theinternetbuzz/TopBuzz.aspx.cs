using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Commands.Trends;

namespace TheInternetBuzz.Web.Pages
{
    public partial class TopBuzz : System.Web.UI.Page
    {
        protected global::TheInternetBuzz.Web.Controls.CloudControl topTrendsCloudHtml;
        protected global::TheInternetBuzz.Web.Controls.TitleControl titleHtml;

        protected void Page_Load(object sender, EventArgs e)
        {
            titleHtml.Title = "Top Buzz";
            topTrendsCloudHtml.TrendsCommand = new YearlyTrendsCommand();
        }
    }
}
