
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Data.Search;

namespace TheInternetBuzz.Commands.Search
{
    public class SearchCommand : ISearchCommand
    {
        private SearchContext searchContext = null;

        public SearchCommand(SearchContext searchContext)
        {
            this.searchContext = searchContext;
        }

        public SearchCommand(SearchTypeEnum searchType, string query, int page, int count)
        {
            this.searchContext = new SearchContext(searchType, query, page, count);
        }

        public SearchResultList Search()
        {
            SearchResultList searchResultList  = new SearchService().Search(searchContext);
            return searchResultList;
        }
    }
}