using System;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Data.Suggestions;

using TheInternetBuzz.Providers.Google;

using TheInternetBuzz.Services.Audit;

namespace TheInternetBuzz.Services.Suggestions
{
    public class SuggestionsService
    {
        public SuggestionsList Suggest(string query)
        {
            AuditServiceItem auditServiceItem = AuditService.Register("SuggestionsService", "Suggest", query);
            AuditService.Start(auditServiceItem);

            SuggestionsList suggestionsList = SuggestionsCacheHelper.ReadSuggestionsList(query);

            if (suggestionsList == null)
            {
                try
                {
                    IProviderSuggestionsService googleSuggestionsService = new GoogleSuggestionsService();
                    SuggestionsContext googleSuggestionsContext = new SuggestionsContext(ProviderEnum.Google, query);

                    suggestionsList = googleSuggestionsService.GetSuggestions(googleSuggestionsContext);

                    SuggestionsCacheHelper.CacheSuggestionList(query, suggestionsList);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("SuggestionsService", "Suggest", query, exception);
                }
            }

            AuditService.End(auditServiceItem);

            return suggestionsList;
        }

    }
}
