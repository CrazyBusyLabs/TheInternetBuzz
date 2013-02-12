using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Services.Categorization;
using TheInternetBuzz.Data.Categorization;
using TheInternetBuzz.Services.Topics;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Data;
using TheInternetBuzz.Web;
using TheInternetBuzz.Commands.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.XML
{
    public partial class Sitemap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DisplayTrends()
        {
            TrendsList trendsList = new TrendsCommand().GetTrends();
            if (trendsList != null && trendsList.Count() > 0)
            {
                foreach (TrendItem trendItem in trendsList)
                {
                    DisplayTrend(trendItem);
                }
            }
        }

        protected void DisplayCategories()
        {
            CategorizationService categorizationService = new CategorizationService();
            CategoriesList categoriesList  = categorizationService.GetCategories();
            foreach (CategoryItem categoryItem in categoriesList)
            {
                TopicList topicList = categoryItem.TopicList;
                foreach (TopicItem topicItem in topicList)
                {
                    DisplayTopic(topicItem);
                }
            }
        }

        private void DisplayTrend(TrendItem trendItem)
        {
            Response.Write("<url>");
            Response.Write("<loc>" + URLBuilder.BuildFullLiveURL("topic", trendItem.Title) + "</loc>");
            Response.Write("<changefreq>hourly</changefreq>");
            Response.Write("<priority>0.5</priority>");
            Response.Write("</url>");
        }

        private void DisplayTopic(TopicItem topicItem)
        {
            Response.Write("<url>");
            Response.Write("<loc>" + URLBuilder.BuildFullLiveURL("topic", topicItem.Title) + "</loc>");
            Response.Write("<changefreq>daily</changefreq>");
            Response.Write("<priority>0.1</priority>");
            Response.Write("</url>");
        }
    }
}
