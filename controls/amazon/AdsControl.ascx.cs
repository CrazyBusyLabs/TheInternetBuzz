using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheInternetBuzz.Data.Ads;

namespace TheInternetBuzz.Web.Controls.Amazon
{
    public partial class AdsControl : UserControl
    {
        public AdsTypeEnum AdsType
        {
            get;
            set;
        }
    }
}