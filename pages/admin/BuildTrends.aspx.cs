using System;
using System.Web;

using TheInternetBuzz.Services.Trends;

namespace TheInternetBuzz.Web.Pages.Admin
{
    public class BuildTrends : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Build()
        {
            new TrendsService().BuildTrends();
        }
    }
}