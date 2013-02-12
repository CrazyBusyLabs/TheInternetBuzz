using System;
using System.Net;
using System.IO;
using System.Xml;
using TheInternetBuzz.Connectors.XML;

namespace TheInternetBuzz.Connectors.RSS
{

    public class RSSConnector {

        public Channel GetChannel(string url)
        {
            string path = "rss/channel/item";

            XmlDocument rssDoc = new XMLConnector().GetXMLDocument(url);

            Channel channel = new Channel();
            XmlNodeList rssItems = rssDoc.SelectNodes(path);

            if (rssItems != null) 
            {
                foreach (XmlNode rssItem in rssItems)
                {
                    Item item = new Item();

                    foreach (XmlNode childNode in rssItem.ChildNodes)

                    if (childNode.Name == "title")
                    {
                        item.Title = childNode.InnerText;
                    }
                    else if (childNode.Name == "link")
                    {
                        item.URL = childNode.InnerText;
                    }
                    else if (childNode.Name == "description")
                    {
                        item.Description = childNode.InnerText;
                    }
                    else
                    {
                        item.Set(childNode.Name, childNode.InnerText);
                    }

                    channel.Add(item);
                }
            }
            return channel;
        }
    }
}