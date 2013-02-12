using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheInternetBuzz.Web.Controls.Twitter
{
    public partial class SearchWidgetControl : UserControl
    {
        public string WidgetSubject
        {
            get;
            set;
        }

        public string WidgetQuery
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}