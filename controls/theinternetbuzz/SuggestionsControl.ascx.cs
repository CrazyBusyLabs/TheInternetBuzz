using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Data.Suggestions;
using TheInternetBuzz.Commands.Suggestions;

namespace TheInternetBuzz.Web.Controls
{
    public partial class SuggestionsControl : AsyncUserControl
    {
        public string Title
        {
            get;
            set;
        }

        public string Query
        {
            get;
            set;
        }

        protected SuggestionsList SuggestionsList
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
            Trace.Write("SuggestionsControl LoadData() - Start Calling Suggestions Command");
            ISuggestionsCommand suggestionsCommand = new SuggestionsCommand(Query);
            SuggestionsList = suggestionsCommand.Suggest();
            Trace.Write("SuggestionsControl LoadData() - End Calling Suggestions Command");
        }

        protected override bool isDisplayControl()
        {
            return SuggestionsList != null && SuggestionsList.Count() > 0;
        }

        protected void DisplaySuggestions()
        {
            foreach (SuggestionItem suggestionItem in SuggestionsList)
            {
                string url = URLBuilder.BuildURL("topic", suggestionItem.Name);
                string htmlEncodedSuggestion = HttpUtility.HtmlEncode(suggestionItem.Name);
                string cssCloudClass = "t" + suggestionItem.Weight;

                Response.Write("<a href=\"" + url + "\" class=\"" + cssCloudClass + "\">" + htmlEncodedSuggestion + "</a> ");
            }
        }
    }
}