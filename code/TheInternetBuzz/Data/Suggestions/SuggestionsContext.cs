using System;
using System.Collections.Generic;
using System.Web;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Suggestions
{
    public class SuggestionsContext
    {
        public ProviderEnum Provider
        {
            get;
            set;
        }

        public string Query
        {
            get;
            set;
        }

        public SuggestionsContext(ProviderEnum provider, string query)
        {
            Provider = provider;
            Query = query;
        }

        public override string ToString()
        {
            return Provider + "|" + Query;
        }
    }
}
