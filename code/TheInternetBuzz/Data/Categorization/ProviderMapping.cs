using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Categorization
{
    public class ProviderMapping
    {
        public ProviderMapping()
        {
        }

        public ProviderMapping(ProviderEnum provider, string category)
        {
            Provider = provider;
            Category = category;
        }

        public ProviderEnum Provider
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }
    }
}
