using System;
using System.Xml;

using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Connectors.XML;

using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Util;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.Yahoo
{
    public class YahooSearchService : IProviderSearchService
    {
        public YahooSearchService()
        {
        }

        public SearchResultList Search(SearchContext searchContext)
        {
            SearchResultList resultList = null;
            switch (searchContext.SearchType)
            {
                case SearchTypeEnum.News:
                    resultList = SearchNews(searchContext);
                    break;

                case SearchTypeEnum.Web:
                    resultList = SearchWeb(searchContext);
                    break;

                default:
                    return resultList;
            }

            return resultList;
        }

        private SearchResultList SearchNews(SearchContext searchContext)
        {
            SearchResultList resultList = new SearchResultList();

            try
            {
                string query = searchContext.Query;
                int page = searchContext.Page;
                int count = searchContext.Count;
                int start = (page - 1) * count;
                string urlTemplate = "http://boss.yahooapis.com/ysearch/news/v1/{0}?appid={1}&start={2}&count={3}&orderby=date&age=7d&style=raw&format=xml";
                string api = ConfigService.GetConfig(ConfigKeys.YAHOO_API_KEY, "");

                string url = string.Format(urlTemplate, query, api, start, count);

                XmlDocument xmlDocument = new XMLConnector().GetXMLDocument(url);
                XmlNamespaceManager nsmanager = new XmlNamespaceManager(xmlDocument.NameTable);
                nsmanager.AddNamespace("default", "http://www.inktomi.com/");

                XmlNodeList resultNodes = xmlDocument.SelectNodes("default:ysearchresponse/default:resultset_news/default:result", nsmanager);
                if (resultNodes != null)
                {
                    foreach (XmlNode resultNode in resultNodes)
                    {
                        SearchResultItem resultItem = new SearchResultItem();
                        resultItem.Provider = ProviderEnum.Yahoo;

                        XmlNode titleNode = resultNode.SelectSingleNode("default:title", nsmanager);
                        if (titleNode != null)
                        {
                            resultItem.Title = titleNode.InnerText;
                        }

                        XmlNode abstractNode = resultNode.SelectSingleNode("default:abstract", nsmanager);
                        if (abstractNode != null)
                        {
                            string abstractText = abstractNode.InnerText;
                            abstractText = TextCleaner.StripTag(abstractText, "<", ">"); // remove any html tags
                            abstractText = TextCleaner.RemoveHtml(abstractText);
                            resultItem.Abstract = abstractText;
                        }

                        XmlNode urlNode = resultNode.SelectSingleNode("default:url", nsmanager);
                        if (urlNode != null)
                        {
                            resultItem.URL = urlNode.InnerText;
                        }

                        XmlNode sourceNode = resultNode.SelectSingleNode("default:source", nsmanager);
                        if (sourceNode != null)
                        {
                            resultItem.Source = sourceNode.InnerText;
                        }

                        XmlNode dateNode = resultNode.SelectSingleNode("default:date", nsmanager);
                        XmlNode timeNode = resultNode.SelectSingleNode("default:time", nsmanager);
                        if (dateNode != null && timeNode != null)
                        {
                            //format <date>2009/11/20</date><time>01:03:57</time> 
                            string dateString = dateNode.InnerText + " " + timeNode.InnerText + " +0";
                            if (dateString != null)
                            {
                                DateTime publishedDate = DateParser.Parse(dateString, "yyyy'/'MM'/'dd HH':'mm':'ss z");
                                resultItem.PublishedDate = publishedDate;
                            }
                        }

                        resultList.Add(resultItem);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("YahooSearchService", "SearchNews", searchContext.ToString(), exception);
            }

            return resultList;
        }

        private SearchResultList SearchWeb(SearchContext searchContext)
        {
            SearchResultList resultList = new SearchResultList();

            try
            {
                string query = searchContext.Query;
                int page = searchContext.Page;
                int count = searchContext.Count;
                int start = (page - 1) * count; 
                string urlTemplate = "http://boss.yahooapis.com/ysearch/web/v1/{0}?appid={1}&start={2}&count={3}&style=raw&format=xml";
                string api = ConfigService.GetConfig(ConfigKeys.YAHOO_API_KEY, "");
                string url = string.Format(urlTemplate, query, api, start, count);

                XmlDocument xmlDocument = new XMLConnector().GetXMLDocument(url);
                XmlNamespaceManager nsmanager = new XmlNamespaceManager(xmlDocument.NameTable);
                nsmanager.AddNamespace("default", "http://www.inktomi.com/");

                XmlNodeList resultNodes = xmlDocument.SelectNodes("default:ysearchresponse/default:resultset_web/default:result", nsmanager);
                if (resultNodes != null)
                {
                    foreach (XmlNode resultNode in resultNodes)
                    {
                        SearchResultItem resultItem = new SearchResultItem();
                        resultItem.Provider = ProviderEnum.Yahoo;

                        XmlNode titleNode = resultNode.SelectSingleNode("default:title", nsmanager);
                        if (titleNode != null)
                        {
                            resultItem.Title = titleNode.InnerText;
                        }

                        XmlNode abstractNode = resultNode.SelectSingleNode("default:abstract", nsmanager);
                        if (abstractNode != null)
                        {
                            string abstractText = abstractNode.InnerText;
                            abstractText = TextCleaner.StripTag(abstractText, "<", ">"); // remove any html tags
                            abstractText = TextCleaner.RemoveHtml(abstractText);
                            resultItem.Abstract = abstractText;
                        }

                        XmlNode urlNode = resultNode.SelectSingleNode("default:url", nsmanager);
                        if (urlNode != null)
                        {
                            resultItem.URL = urlNode.InnerText;
                        }

                        XmlNode dateNode = resultNode.SelectSingleNode("default:date", nsmanager);
                        if (dateNode != null)
                        {
                            //format <date>2009/11/20</date><time>01:03:57</time> 
                            string dateString = dateNode.InnerText;
                            if (dateString != null)
                            {
                                DateTime publishedDate = DateParser.Parse(dateString, "yyyy'/'MM'/'dd");
                                resultItem.PublishedDate = publishedDate;
                            }
                        }

                        resultList.Add(resultItem);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("YahooSearchService", "SearchWeb", searchContext.ToString(), exception);
            }

            return resultList;
        }

    } 
}
