using System;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using System.Web;
using TheInternetBuzz.Connectors.XML;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Topics;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.Wikipedia
{
    public class WikipediaTopicService : IProviderTopicService
    {
        public WikipediaTopicService()
        {
        }

        public void FillTopic(TopicItem topicItem)
        {
            String query = topicItem.Query.Replace(" ", "_");

            try
            {
                XMLConnector XMLConnector = new XMLConnector();

                bool isRedirect = true;

                while (isRedirect) 
                {

                    isRedirect = false;

                    string url = "http://en.wikipedia.org/wiki/Special:Export/" + query;
                    XPathDocument xpathdoc = XMLConnector.GetXPathDocument(url);

                    String NS = "http://www.mediawiki.org/xml/export-0.4/";
                    XPathNavigator myXPathNavigator = xpathdoc.CreateNavigator();
                    XPathNodeIterator nodesIt = myXPathNavigator.SelectDescendants("text", NS, false);

                    string content = null;
                    while (nodesIt.MoveNext())
                    {
                        content = content + nodesIt.Current.InnerXml;
                    }
                    if (content != null && content.Length > 0)
                    {
                        // "#REDIRECT [[Dan Quayle]] {{R from other capitalisation}}"
                        if (content.IndexOf("#REDIRECT") >= 0) 
                        {
                            string newQuery = ParseRedirect(content);
                            if (newQuery != null && newQuery.Length > 0)
                            {
                                if (!newQuery.Equals(query))
                                {
                                    query = newQuery;
                                    isRedirect = true;
                                }
                            }
                        }
                        else 
                        {
                            topicItem.WikipediaSummary = BuildSummary(content);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("WikipediaTopicService", "FillTopic", topicItem.ToString(), exception);
            }
        }

        private string BuildSummary(string content)
        {
            string summary = null;

            int startPos = 0;
            int endPos = content.IndexOf("==");

            if (endPos > startPos)
            {
                summary = content.Substring(startPos, endPos - startPos);
                summary = new WikipediaTopicParser().Parse(summary);
            }

            return summary;
        }

        // "#REDIRECT [[Dan Quayle]] {{R from other capitalisation}}"
        private string ParseRedirect(string content)
        {
            string query = null;

            int startPos = content.IndexOf("[[");
            int endPos = content.IndexOf("]]");

            if (endPos > startPos)
            {
                startPos = startPos + "[[".Length; 
                query = content.Substring(startPos, endPos - startPos);
            }

            return query;
        }

    }
}