using System;

using TheInternetBuzz.Data.Suggestions;

namespace TheInternetBuzz.Services.Suggestions
{
    public interface IProviderSuggestionsService
    {
        SuggestionsList GetSuggestions(SuggestionsContext suggestionsContext);
    }
}
