using System;

using TheInternetBuzz.Data.Suggestions;

namespace TheInternetBuzz.Commands.Suggestions
{
    public interface ISuggestionsCommand
    {
        SuggestionsList Suggest();
    }
}