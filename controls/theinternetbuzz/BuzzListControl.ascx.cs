using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Web;
using TheInternetBuzz.Commands.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Web.Controls
{
    public partial class BuzzListControl : AsyncUserControl
    {
        public string Title
        {
            get;
            set;
        }

        public int MaximumWeight
        {
            get;
            set;
        }

        public int MimimumWeight
        {
            get;
            set;
        }

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
            Trace.Write("BuzzListControl LoadData() - Start Calling Trends Command");
            if (TrendsCommand != null)
            {
                TrendsList = TrendsCommand.GetTrends();
            }
            Trace.Write("BuzzListControl LoadData() - End Calling Trends Command");
        }

        protected override bool isDisplayControl()
        {
            return TrendsList != null;
        }

        protected void DisplayBuzzList()
        {
            if (TrendsList != null)
            {
                foreach (TrendItem trendItem in TrendsList)
                {
                    if (trendItem.Weight >= MimimumWeight && trendItem.Weight <= MaximumWeight)
                    {
                        string cssCloudClass = "t2";
                        string htmlEncodedTrend = HttpUtility.HtmlEncode(trendItem.Title);
                        string url = URLBuilder.BuildURL("topic", trendItem.Title);

                        Response.Write("<a href=\"" + url + "\" class=\"" + cssCloudClass + "\">" + htmlEncodedTrend + "</a> ");
                    }
                }
            }
        }
    }
}