using System;
using System.Web;
using System.Web.UI;

namespace TheInternetBuzz.Web.Controls.Amazon
{
    public partial class MP3Control : UserControl
    {
        public string Query
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isVisible = false;

            isVisible = "Lady Gaga".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Jessica Simpson".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Justin Bieber".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Rihanna".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Michael Jackson".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Susan Boyle".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Adam Lambert".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Black Eyed Peas".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Ke$Ha".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Ke$Ha".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Owl City".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Phoenix".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Robin Thicke".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Maroon 5".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Michael Bublé".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Alicia Keys".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Taylor Swift".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Miley Cyrus".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Imagine Dragons".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Daft Punk".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Florida Georgia Line".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "OneRepublic".Equals(Query, StringComparison.CurrentCultureIgnoreCase) ||
                "Selena Gomez".Equals(Query, StringComparison.CurrentCultureIgnoreCase);

            this.Visible = isVisible;
        }
    }
}