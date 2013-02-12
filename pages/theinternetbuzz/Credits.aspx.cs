using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Commands.Trends;

namespace TheInternetBuzz.Web.Pages
{
    public class Credits : System.Web.UI.Page
    {
        protected global::TheInternetBuzz.Web.Controls.TitleControl titleHtml;

        protected void Page_Load(object sender, EventArgs e)
        {
            titleHtml.Title = "Credits";
        }
    }
}
