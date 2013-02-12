using System;

using TheInternetBuzz.Data.Suggestions;
using TheInternetBuzz.Services.Suggestions;

namespace TheInternetBuzz.Commands.Suggestions
{
    public class SuggestionsCommand : ISuggestionsCommand
    {
        private string query = null;

        public SuggestionsCommand(string query)
        {
            this.query = query;
        }

        public SuggestionsList Suggest()
        {
            return new SuggestionsService().Suggest(query);
        }
    }
}