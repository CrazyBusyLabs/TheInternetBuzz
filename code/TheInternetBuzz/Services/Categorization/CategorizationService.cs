using System;
using System.Web;
using System.Xml;

using TheInternetBuzz.Connectors.XML;
using TheInternetBuzz.Data;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Topics;
using TheInternetBuzz.Data.Categorization;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Services.Categorization
{
    public class CategorizationService
    {
        public CategoriesList GetCategories()
        {
            CategoriesList categoriesList = CategorizationCacheHelper.ReadCategoriesList();

            if (categoriesList == null)
            {
                try
                {
                    categoriesList = BuildCategories();

                    CategorizationCacheHelper.CacheCategoriesList(categoriesList);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("CategorizationService", "GetCategories", null, exception);
                }
            }

            return categoriesList;
        }

        public TrendsList GetTrends()
        {
            TrendsList trendsList = CategorizationCacheHelper.ReadTrends();

            if (trendsList == null)
            {
                trendsList = new TrendsList();

                CategoriesList categoriesList = GetCategories();
                foreach (CategoryItem categoryItem in categoriesList)
                {
                    TopicList topicList = categoryItem.TopicList;
                    foreach (TopicItem topicItem in topicList)
                    {
                        TrendItem trendItem = new TrendItem(topicItem.ID, topicItem.Title, ProviderEnum.TheInternetBuzz);
                        trendItem.Weight = 3;
                        trendsList.AddTrend(trendItem);
                    }
                }
                trendsList.Sort();

                CategorizationCacheHelper.CacheTrends(trendsList);
            }
            return trendsList;
        }

        private CategoriesList BuildCategories()
        {
            CategoriesList categoriesList = new CategoriesList();

            try
            {
                string filepath =  System.Web.Hosting.HostingEnvironment.MapPath("~/data/Categories.xml"); 
                XmlDocument xmlDocument = new XMLConnector().GetXMLDocument(filepath);

                XmlNodeList categoryNodes = xmlDocument.SelectNodes("categories/category");
                if (categoryNodes != null)
                {
                    foreach (XmlNode categoryNode in categoryNodes)
                    {
                        string id = categoryNode.Attributes["id"].Value;                        
                        CategoryItem categoryItem = new CategoryItem(id);
                        categoryItem.Title = categoryNode.Attributes["title"].Value;

                        XmlNodeList providerMappingsNodes = categoryNode.SelectNodes("provider-mappings/provider-mapping");
                        if (providerMappingsNodes != null)
                        {
                            ProviderMappingList providerMappingList = categoryItem.ProviderMappingList;
                            foreach (XmlNode providerMappingNode in providerMappingsNodes)
                            {
                                ProviderMapping providerMapping = new ProviderMapping();
                                providerMapping.Provider = (ProviderEnum) Enum.Parse(typeof(ProviderEnum), providerMappingNode.Attributes["id"].Value);
                                providerMapping.Category = providerMappingNode.Attributes["category"].Value;

                                providerMappingList.Add(providerMapping);
                            }
                        }

                        XmlNodeList topicsNodes = categoryNode.SelectNodes("topics/topic");
                        if (topicsNodes != null)
                        {
                            TopicList topicList = categoryItem.TopicList;
                            foreach (XmlNode topicNode in topicsNodes)
                            {
                                string title = topicNode.Attributes["title"].Value;
                                TopicItem topicItem = new TopicItem(title, title);
                                topicList.Add(topicItem);
                            }
                        }

                        categoriesList.Add(categoryItem);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("CategorizationService", "BuildCategories", null, exception);
            }

            return categoriesList;
        }
    }
}