using System;
using System.Web;
using System.Web.UI;

using TheInternetBuzz.Web;
using TheInternetBuzz.Commands.Categorization;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Data.Categorization;

namespace TheInternetBuzz.Web.Controls
{
    public partial class CategoryListControl : AsyncUserControl
    {
        protected CategoriesList CategoriesList
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
            Trace.Write("CategoriesList LoadData() - Start calling Categorization Command");
            ICategorizationCommand categorizationCommand = new CategorizationCommand();
            CategoriesList = categorizationCommand.GetCategories();
            Trace.Write("CategoriesList LoadData() - End calling Categorization Command");
        }

        protected override bool isDisplayControl()
        {
            return CategoriesList != null && CategoriesList.Count() > 0;
        }

        protected void DisplayCategories()
        {
            foreach (CategoryItem categoryItem in CategoriesList)
            {
                DisplayCategory(categoryItem);
            }

        }

        private void DisplayCategory(CategoryItem categoryItem)
        {
            Response.Write("<div class=\"categoryBlock\">");
            Response.Write("<div class=\"categoryTitle\"><h2>" + categoryItem.Title + "</h2></div>");
            Response.Write("<div class=\"categoryBulletList\">");
            Response.Write("<ul>");

            TopicList topicList = categoryItem.TopicList;
            foreach (TopicItem topicItem in topicList)
            {
                DisplayTopic(topicItem);
            }

            Response.Write("</ul>");
            Response.Write("</div>");
            Response.Write("</div>"); 
        }

        private void DisplayTopic(TopicItem topicItem)
        {
            string htmlEncodedTopic = HttpUtility.HtmlEncode(topicItem.Title);
            string url = URLBuilder.BuildURL("topic", topicItem.Title);

            Response.Write("<li><a href=\"" + url + "\">" + htmlEncodedTopic + "</a></li>");
        }
    }
}

