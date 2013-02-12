using System;
using System.Collections;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Suggestions
{
    public class SuggestionsList : List 
    {
        public void AddSuggestions(SuggestionsList suggestionsList)
        {
            foreach (SuggestionItem newSuggestion in suggestionsList)
            {
                SuggestionItem suggestion = GetSuggestion(newSuggestion.Name);
                if (suggestion == null)
                {
                    Add(newSuggestion);
                }
                else
                {
                    if (newSuggestion.Weight > suggestion.Weight)
                    {
                        suggestion.Weight = newSuggestion.Weight;
                    }
                }
            }
        }

        public bool ContainsTrend(string name)
        {
            return GetSuggestion(name) != null;
        }


        public SuggestionItem GetSuggestion(string name)
        {
            SuggestionItem foundSuggestionItem = null;

            foreach (SuggestionItem suggestionItem in this)
            {
                if (suggestionItem.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundSuggestionItem = suggestionItem;
                    break;
                }
            }

            return foundSuggestionItem;
        }
    }
}
