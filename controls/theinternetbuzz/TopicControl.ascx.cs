using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Web;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Commands.Topics;

namespace TheInternetBuzz.Web.Controls
{
    public class TopicControl : AsyncUserControl
    {
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl freebaseHtml;
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl topicAliasesHtml;
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl topicSummaryThumbmailHtml;
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl wikipediaHtml;

        public string Query
        {
            get;
            set;
        }

        public String DisplayQuery
        {
            get;
            set;
        }

        protected TopicItem TopicItem
        {
            get;
            set;
        }

        public string TrackSection
        {
            get;
            set;
        }

        public string TrackTopic
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
            Trace.Write("TopicControl LoadData() - Start Calling Topic Command");
            TopicItem = new TopicCommand(Query).GetTopic();
            Trace.Write("TopicControl LoadData() - End Calling Topic Command");
        }

        protected override bool isDisplayControl()
        {
            freebaseHtml.Visible = TopicItem != null && TopicItem.FreebaseSummary != null && TopicItem.FreebaseSummary.Length > 0;
            topicSummaryThumbmailHtml.Visible = TopicItem != null && TopicItem.FreebaseImageURL != null && TopicItem.FreebaseImageURL.Length > 0;
            topicAliasesHtml.Visible = TopicItem != null && TopicItem.FreebaseAliases != null && TopicItem.FreebaseAliases.Length > 0;

            if (freebaseHtml.Visible)
            {
                wikipediaHtml.Visible = false;
            }
            else
            {
                wikipediaHtml.Visible = TopicItem != null && TopicItem.WikipediaSummary != null && TopicItem.WikipediaSummary.Length > 0;
            }
            return freebaseHtml.Visible || wikipediaHtml.Visible;
        }

        protected void DisplayLinks()
        {
            if (TopicItem.FacebookURL != null && TopicItem.FacebookURL.Length > 0)
            {
                DisplayLink(TopicItem.FacebookURL, "images/icons/facebook.png", "Facebook");
            }
            if (TopicItem.MySpaceURL != null && TopicItem.MySpaceURL.Length > 0)
            {
                DisplayLink(TopicItem.MySpaceURL, "images/icons/myspace.png", "MySpace");
            }
            if (TopicItem.TwitterURL != null && TopicItem.TwitterURL.Length > 0)
            {
                DisplayLink(TopicItem.TwitterURL, "images/icons/twitter.png", "Twitter");
            }
            if (TopicItem.WikipediaURL != null && TopicItem.WikipediaURL.Length > 0)
            {
                DisplayLink(TopicItem.WikipediaURL, "images/icons/wikipedia.png", "Wikipedia");
            }
        }
        
        private void DisplayLink(string url, string icon, string alt)
        {
            Response.Write("<a href=\"" + url + "\" onmousedown=\"trackEvent('" + TrackSection + "','click','" + HttpUtility.HtmlEncode(TrackTopic.Replace("'", @"\'")) + "');\"><img src=\"" + URLBuilder.BuildResourceURL(icon) + "\" alt=\"" + alt + "\"></a> ");
        }

        protected void DisplayAliases()
        {
            Response.Write(string.Join(", ", TopicItem.FreebaseAliases));
        }
    }
}