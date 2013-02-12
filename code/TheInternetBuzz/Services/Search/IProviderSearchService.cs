using System;

using TheInternetBuzz.Data.Search;

namespace TheInternetBuzz.Services.Search
{
    public interface IProviderSearchService
    {
        SearchResultList Search(SearchContext searchContext);
    }
}
