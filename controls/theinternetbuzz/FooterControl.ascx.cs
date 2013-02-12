using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Services.Location;
using TheInternetBuzz.Data.Location;

namespace TheInternetBuzz.Web.Controls
{
    public partial class FooterControl : UserControl
    {
        public TheInternetBuzz.Data.Location.Location Location
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LocationService locationService = new LocationService();
            Location = locationService.GetLocation();
        }
    }
}
