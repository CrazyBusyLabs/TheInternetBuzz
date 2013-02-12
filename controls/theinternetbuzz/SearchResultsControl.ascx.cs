using System;
using System.Web;
using System.Web.UI;

using TheInternetBuzz.Commands.Search;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Web;

namespace TheInternetBuzz.Web.Controls
{
    public partial class SearchResultsControl : AsyncUserControl
    {
        public ISearchCommand SearchCommand
        {
            get;
            set;
        }

        public int PaginationIndex
        {
            get;
            set;
        }

        public int PaginationMax
        {
            get;
            set;
        }

        public string PaginationQuery
        {
            get;
            set;
        }

        public string PaginationSection
        {
            get;
            set;
        }

        public bool DisplayPagination
        {
            get;
            set;
        }

        public bool DisplayDate
        {
            get;
            set;
        }

        public bool DisplaySource
        {
            get;
            set;
        }

        public bool DisplayURL
        {
            get;
            set;
        }

        public bool DisplayImageURL
        {
            get;
            set;
        }

        protected SearchResultList SearchResultList
        {
            get;
            set;
        }

        public string TrackSection
        {
            get;
            set;
        }

        public string TrackTopic
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
            Trace.Write("SearchResultsControl LoadData() - Start Calling Search Command");
            SearchResultList = SearchCommand.Search();
            Trace.Write("SearchResultsControl LoadData() - End Calling Search Command");
        }

        protected override bool isDisplayControl()
        {
            return SearchResultList != null && SearchResultList.Count() > 0;
        }
    }

}