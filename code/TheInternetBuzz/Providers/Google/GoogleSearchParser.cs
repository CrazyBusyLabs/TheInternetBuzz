using System;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Providers.Google
{
    public class GoogleSearchParser 
    {

// INCORRECT RESULT
//{
//  "responseData": null,
//  "responseDetails": "Quota Exceeded.  Please see http://code.google.com/apis/websearch",
//  "responseStatus": 403
//}

// CORRECT RESULT
//        {
//  "responseData": {
//    "results": [
//      {
//        "GsearchResultClass": "GwebSearch",
//        "unescapedUrl": "http://en.wikipedia.org/wiki/Stefan_Savi%C4%87",
//        "url": "http://en.wikipedia.org/wiki/Stefan_Savi%25C4%2587",
//        "visibleUrl": "en.wikipedia.org",
//        "cacheUrl": "http://www.google.com/search?q=cache:HWRCbR1U9oEJ:en.wikipedia.org",
//        "title": "<b>Stefan Savi?</b> - Wikipedia, the free encyclopedia",
//        "titleNoFormatting": "Stefan Savi? - Wikipedia, the free encyclopedia",
//        "content": "<b>Stefan Savi?</b> (Cyrillic: ?????? ?????; born 8 January 1991) is a Montenegrin   footballer who plays for Manchester City in the Premier League. A centre-back <b>...</b>"
//      }
//    ],
//    "cursor": {
//      "resultCount": "55,500",
//      "pages": [
//        {
//          "start": "0",
//          "label": 1
//        }
//      ],
//      "estimatedResultCount": "55500",
//      "currentPageIndex": 0,
//      "moreResultsUrl": "http://www.google.com/search?oe=utf8&ie=utf8&source=uds&start=0&hl=en&q=Stefan+Savic",
//      "searchResultTime": "0.18"
//    }
//  },
//  "responseDetails": null,
//  "responseStatus": 200
//}


        public void Parse(JObject searchResultsJSONObject, SearchResultList searchResultList)
        {
            try 
            {
                JObject responseDataJObject = searchResultsJSONObject.Value<JObject>("responseData");
                if (responseDataJObject != null)
                {
                    JArray resultsJArray = responseDataJObject.Value<JArray>("results");
                    foreach (JObject resultJObject in resultsJArray)
                    {
                        SearchResultItem resultItem = new SearchResultItem();
                        resultItem.Provider = ProviderEnum.Google;

                        string title = resultJObject.Value<string>("titleNoFormatting");
                        resultItem.Title = TextCleaner.RemoveHtml(title);

                        string content = resultJObject.Value<string>("content");
                        resultItem.Abstract = TextCleaner.RemoveHtml(content);

                        string url = resultJObject.Value<string>("unescapedUrl");
                        resultItem.URL = TextCleaner.RemoveHtml(url);

                        string publisher = resultJObject.Value<string>("publisher");
                        if (publisher != null)
                        {
                            resultItem.Source = publisher;
                        }

                        string publishedDateValue = resultJObject.Value<string>("publishedDate");
                        if (publishedDateValue != null)
                        {
                            // format: Thu, 19 Nov 2009 13:05:26 -08:00
                            DateTime publishedDate = DateParser.Parse(publishedDateValue, "ddd, dd MMM yyyy HH':'mm':'ss zzz");
                            resultItem.PublishedDate = publishedDate;
                        }

                        searchResultList.Add(resultItem);

                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("GoogleSearchParser", "Search", searchResultList.ToString(), exception);
            }
        }
    }
}
