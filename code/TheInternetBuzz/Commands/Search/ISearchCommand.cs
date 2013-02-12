using System;

using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Data.Search;

namespace TheInternetBuzz.Commands.Search
{
    public interface ISearchCommand
    {
        SearchResultList Search();
    }
}