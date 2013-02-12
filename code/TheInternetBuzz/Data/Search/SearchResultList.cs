using System;
using System.Collections;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Search
{
    public class SearchResultList : List 
    {
        public void AddResults(SearchResultList resultList)
        {
            foreach (SearchResultItem newResult in resultList)
            {
                SearchResultItem result = GetResult(newResult.URL);
                if (result == null)
                {
                    Add(newResult);
                }
            }
        }


        public SearchResultItem GetResult(string url)
        {
            SearchResultItem foundResultItem = null;

            foreach (SearchResultItem resultItem in this)
            {
                if (resultItem.URL.Equals(url, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundResultItem = resultItem;
                    break;
                }
            }

            return foundResultItem;
        }
    }
}
