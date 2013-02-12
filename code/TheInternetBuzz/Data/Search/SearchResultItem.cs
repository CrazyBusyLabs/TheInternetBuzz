using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Search
{
    public class SearchResultItem
    {
        private string url = null;

        public SearchResultItem()
        {
            PublishedDate = DateTime.Now;
        }

        public ProviderEnum Provider
        {
            get;
            set;
        }

        public string Title 
        {
            get;
            set;
        }

        public string Abstract
        {
            get;
            set;
        }

        public string URL
        {
            get { return url;  }
            set { url = CleanURL(value); }
        }

        public string ImageURL
        {
            get;
            set;
        }

        public string DisplayURL
        {
            get
            {
                string displayURL = null;
                if (URL != null)
                {
                    if (URL.IndexOf("?") > 0)
                    {
                        displayURL = URL.Substring(0, URL.IndexOf("?") - 1);
                    }
                    else
                    {
                        displayURL = URL;
                    }
                }
                return displayURL;
            }
        }

        public DateTime PublishedDate
        {
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        protected string CleanURL(string url)
        {
            string newURL = url;
            newURL = newURL.Replace("\"", "");
            return newURL;
        }
    }
}
