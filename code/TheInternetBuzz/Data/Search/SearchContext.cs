using System;
using System.Collections.Generic;
using System.Web;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Search
{
    public class SearchContext
    {
        public SearchContext(string query, int page, int count)
        {
            SearchType = SearchTypeEnum.Web;
            Query = query;
            Page = page;
            Count = count;
        }

        public SearchContext(SearchTypeEnum searchType, string query, int page, int count)
        {
            SearchType = searchType;
            Query = query;
            Page = page;
            Count = count;
        }

        public SearchContext(ProviderEnum provider, SearchTypeEnum searchType, string query, int page, int count)
        {
            Provider = provider;
            SearchType = searchType;
            Query = query;
            Page = page;
            Count = count;
        }

        public ProviderEnum Provider
        {
            get;
            set;
        }

        public SearchTypeEnum SearchType
        {
            get;
            set;
        }

        public string Query
        {
            get;
            set;
        }

        public int Page
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        public override string ToString()
        {
            return SearchType + "|" + Query + "|" + Page;
        }
    }
}
