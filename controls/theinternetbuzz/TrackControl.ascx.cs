using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheInternetBuzz.Web.Controls
{
    public partial class TrackControl : UserControl
    {
        public string TrackerCategory
        {
            get;
            set;
        }

        public string TrackerAction
        {
            get;
            set;
        }

        public string TrackerLabel
        {
            get;
            set;
        }
    }
}