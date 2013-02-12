using System;

using TheInternetBuzz.Data.Suggestions;
using TheInternetBuzz.Connectors.XML;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Suggestions;
using System.Xml;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Web;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.Google
{
    public class GoogleSuggestionsService : IProviderSuggestionsService
    {
        public GoogleSuggestionsService()
        {
        }

        public SuggestionsList GetSuggestions(SuggestionsContext suggestionsContext)
        {
            SuggestionsList suggestionsList = new SuggestionsList();
            try
            {
                string query = suggestionsContext.Query;

                string urlTemplate = "http://google.com/complete/search?output=toolbar&q={0}";
                string url = string.Format(urlTemplate, query);

                XmlDocument xmlDocument = new XMLConnector().GetXMLDocument(url); ;

                XmlNodeList resultNodes = xmlDocument.SelectNodes("toplevel/CompleteSuggestion");
                if (resultNodes != null)
                {
                    foreach (XmlNode resultNode in resultNodes)
                    {
                        string name = null;
                        double numberQueries = 0;

                        XmlNode suggestionNode = resultNode.SelectSingleNode("suggestion");
                        if (suggestionNode != null)
                        {
                            XmlAttribute dataAttribute = suggestionNode.Attributes["data"];
                            if (dataAttribute != null)
                            {
                                name = dataAttribute.Value;
                            }
                        }

                        XmlNode numQueriesNode = resultNode.SelectSingleNode("num_queries");
                        if (numQueriesNode != null)
                        {
                            XmlAttribute intAttribute = numQueriesNode.Attributes["int"];
                            if (intAttribute != null)
                            {
                                string numQuery = intAttribute.Value;
                                try {
                                    numberQueries = double.Parse(numQuery);
                                } catch (Exception) {}
                            }
                        }

                        if (name != null && numberQueries > 0)
                        {
                            SuggestionItem suggestionItem = new SuggestionItem(name);
                            suggestionItem.Provider = ProviderEnum.Google;
                            suggestionItem.NumberQueries = numberQueries;
                            suggestionItem.Weight = CalculateWeight(numberQueries);
                            suggestionsList.Add(suggestionItem);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("GoogleSuggestionsService", "GetSuggestions", suggestionsContext.ToString(), exception);
            }

            return suggestionsList;
        }

        private short CalculateWeight(double value)
        {
            short weight = 1;

            if (value > 50000000)
            {
                weight = 5;
            }
            else if (value > 10000000)
            {
                weight = 4;

            }
            else if (value > 1000000)
            {
                weight = 3;
            }
            else if (value > 250000)
            {
                weight = 2;

            }
            else
            {
                weight = 1;
            }

            return weight;
        }
    }
}
