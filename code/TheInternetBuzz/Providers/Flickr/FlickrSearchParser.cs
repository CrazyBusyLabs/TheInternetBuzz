using System;
using System.Collections;

using System.Xml;
using System.Xml.XPath;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Providers.Flickr
{
    public class FlickrSearchParser 
    {
        public void Parse(XmlDocument xmlDocument, SearchResultList searchResultList)
        {
            string urlTemplate = "http://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg";

            try 
            {
                XmlNodeList resultNodes = xmlDocument.SelectNodes("rsp/photos/photo");
                if (resultNodes != null)
                {
                    foreach (XmlNode resultNode in resultNodes)
                    {
                        Console.WriteLine("resultNode");

                        SearchResultItem resultItem = new SearchResultItem();
                        resultItem.Provider = ProviderEnum.Flickr;

                        string id = resultNode.Attributes["id"].Value;
                        string secret = resultNode.Attributes["secret"].Value;
                        string server = resultNode.Attributes["server"].Value;
                        string farm = resultNode.Attributes["farm"].Value;
                        string title = resultNode.Attributes["title"].Value;
                        string url = string.Format(urlTemplate, farm, server, id, secret);

                        resultItem.ImageURL = url;
                        resultItem.Title = title;

                        Console.WriteLine(url);

                        searchResultList.Add(resultItem);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("FlickrSearchParser", "Search", searchResultList.ToString(), exception);
            }
        }
    }
}
