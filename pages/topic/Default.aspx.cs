using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Commands.Search;
using TheInternetBuzz.Commands.Topics;
using TheInternetBuzz.Commands.Video;
using TheInternetBuzz.Commands.Trends;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Web.Pages.Topic
{
    public class Default : System.Web.UI.Page
    {
        protected global::TheInternetBuzz.Web.Controls.HtmlHeadControl headHtml;
        protected global::TheInternetBuzz.Web.Controls.HtmlPostBodyControl postBodyHtml;
        protected global::TheInternetBuzz.Web.Controls.NavigationControl navigationHtml;
        protected global::TheInternetBuzz.Web.Controls.HeaderToolbarControl toolbarHtml;
        protected global::TheInternetBuzz.Web.Controls.TitleControl titleHtml;
        protected global::TheInternetBuzz.Web.Controls.TopicControl topicHtml;
        protected global::TheInternetBuzz.Web.Controls.SearchResultsControl newsResultsHtml;
        protected global::TheInternetBuzz.Web.Controls.SearchResultsControl webResultsHtml;
        protected global::TheInternetBuzz.Web.Controls.SearchResultsControl tweetsResultsHtml;
        protected global::TheInternetBuzz.Web.Controls.Amazon.MP3Control MP3Html;
        protected global::TheInternetBuzz.Web.Controls.FooterControl footerHtml;
        protected global::TheInternetBuzz.Web.Controls.TrackControl trackerHtml;
        protected global::TheInternetBuzz.Web.Controls.VideoControl videoHtml;
        protected global::TheInternetBuzz.Web.Controls.WhatTheTrend.ExplanationControl explanationHtml;
        protected global::TheInternetBuzz.Web.Controls.BuzzListControl buzzListHtml;
        protected global::TheInternetBuzz.Web.Controls.Google.AdsControl googleAd0Html;
        protected global::TheInternetBuzz.Web.Controls.Google.AdsControl googleAd1Html;
        protected global::TheInternetBuzz.Web.Controls.Google.AdsControl googleAd2Html;

        public string Query
        {
            get;
            private set;
        }

        public bool IsTrend
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["topic"] == null || Request.QueryString["topic"].Length == 0)
            {
                Query = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_DEFAULT_QUERY, "");
            }
            else
            {
                Query = Request.QueryString["topic"];
                TrendsList trendsList = new TrendsCommand().GetTrends();
                if (trendsList != null && trendsList.Count() > 0)
                {
                    IsTrend = trendsList.IsTrend(Query);
                }
                if (!IsTrend)
                {
                    TrendsList yearlyTrendsList = new YearlyTrendsCommand().GetTrends();
                    if (yearlyTrendsList != null && yearlyTrendsList.Count() > 0)
                    {
                        IsTrend = yearlyTrendsList.IsTrend(Query);
                    }
                }
            }

            headHtml.Title = "The Internet Buzz | Topic | " + Query;
            headHtml.Description = "Facts, news, buzz and informations about " + Query + " from TheInternetBuzz, the real-time Internet Buzz engine.";
            headHtml.Keywords = Query;
            headHtml.RobotIndexing = IsTrend;

            titleHtml.Title = HttpUtility.HtmlEncode(Query);

            toolbarHtml.DefaultSearchValue = HttpUtility.HtmlEncode(Query);

            topicHtml.Query = Query;
            topicHtml.DisplayQuery = HttpUtility.HtmlEncode(Query);
            topicHtml.TrackSection = "topic";
            topicHtml.TrackTopic = Query;

            explanationHtml.Query = Query;
            explanationHtml.DisplayQuery = HttpUtility.HtmlEncode(Query);

            int pageMax = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_COUNT_PER_PAGE_PER_PROVIDER, 8);

            tweetsResultsHtml.SearchCommand = new SearchCommand(SearchTypeEnum.Tweet, Query, 1, pageMax);
            tweetsResultsHtml.DisplayDate = true;
            tweetsResultsHtml.DisplaySource = false;
            tweetsResultsHtml.DisplayURL = true;
            tweetsResultsHtml.DisplayImageURL = true;
            tweetsResultsHtml.DisplayPagination = false; ;
            tweetsResultsHtml.TrackSection = "topic";
            tweetsResultsHtml.TrackTopic = Query;

            //webResultsHtml.SearchCommand = new SearchCommand(SearchTypeEnum.Web, Query, 1, pageMax);
            //webResultsHtml.DisplayDate = false;
            //webResultsHtml.DisplaySource = true;
            //webResultsHtml.DisplayURL = true;
            //webResultsHtml.DisplayPagination = false; ;
            //webResultsHtml.TrackSection = "topic";
            //webResultsHtml.TrackTopic = Query;

            //newsResultsHtml.SearchCommand = new SearchCommand(SearchTypeEnum.News, Query, 1, pageMax);
            //newsResultsHtml.DisplayDate = true;
            //newsResultsHtml.DisplaySource = true;
            //newsResultsHtml.DisplayPagination = false; ;
            //newsResultsHtml.TrackSection = "topic";
            //newsResultsHtml.TrackTopic = Query;

            MP3Html.Query = Query;

            buzzListHtml.Title = "Buzzing Now";
            buzzListHtml.MaximumWeight = 5;
            buzzListHtml.MimimumWeight = 5;
            buzzListHtml.TrendsCommand = new TrendsCommand();

            trackerHtml.TrackerCategory = "topic";
            trackerHtml.TrackerAction = "view";
            trackerHtml.TrackerLabel = Query;

            videoHtml.VideoCommand = new VideoSearchCommand(Query);

            googleAd0Html.Visible = IsTrend;
            googleAd1Html.Visible = IsTrend;
            googleAd2Html.Visible = IsTrend;
        }

    }
}