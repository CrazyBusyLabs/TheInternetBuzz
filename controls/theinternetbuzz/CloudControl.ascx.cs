using System;
using System.Web;
using System.Web.UI;

using TheInternetBuzz.Web;
using TheInternetBuzz.Commands.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Web.Controls
{
    public partial class CloudControl : AsyncUserControl
    {
        public ITrendsCommand TrendsCommand
        {
            get;
            set;
        }

        public TrendsList TrendsList
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
            Trace.Write("TrendsControl LoadData() - Start Calling Trends Command");
            if (TrendsCommand != null)
            {
                TrendsList = TrendsCommand.GetTrends();
            }
            Trace.Write("TrendsControl LoadData() - End Calling Trends Command");
        }

        protected override bool isDisplayControl()
        {
            return TrendsList != null;
        }

        protected void DisplayTrends()
        {
            if (TrendsList != null)
            {
                foreach (TrendItem trendItem in TrendsList)
                {
                    string cssCloudClass = "t" + trendItem.Weight;
                    string htmlEncodedTrend = HttpUtility.HtmlEncode(trendItem.Title);
                    string url = URLBuilder.BuildURL("topic", trendItem.Title);

                    Response.Write("<a href=\"" + url + "\" class=\"" + cssCloudClass + "\">" + htmlEncodedTrend + "</a> ");
                }
            }
        }
    }
}

