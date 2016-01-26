using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Web;
using TheInternetBuzz.Data.Explanation;
using TheInternetBuzz.Commands.Explanation;

namespace TheInternetBuzz.Web.Controls.WhatTheTrend
{
    public partial class ExplanationControl : AsyncUserControl
    {
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

        public ExplanationItem ExplanationItem
        {
            get;
            set;
        }

        public string TopicURL
        {
            get;
            set;
        }

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
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            TopicURL = URLBuilder.BuildFullLiveURL("topic", Query);
        }

        protected override void LoadData()
        {
            Trace.Write("DefinitionControl LoadData() - Start Calling Explanation Command");

            if (isBot())
            {
                Trace.Write("DefinitionControl LoadData() - Not calling explanation command for Bot");                
            }
            else
            {
                ExplanationItem = new ExplanationCommand(Query).GetExplanation();
            }

            Trace.Write("DefinitionControl LoadData() - End Calling Explanation Command");
        }

        protected override bool isDisplayControl()
        {
            return true;
        }
    }
}