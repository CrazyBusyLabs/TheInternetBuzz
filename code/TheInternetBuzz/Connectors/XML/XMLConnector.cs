using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using TheInternetBuzz.Connectors.HTTP;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Connectors.XML
{
    public class XMLConnector 
    {
        public XmlDocument GetXMLDocument(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                if (path.StartsWith("http"))
                {
                    HttpWebRequest request = new HTTPConnector().BuildWebRequest(path, "text/xml");
                    using (WebResponse response = request.GetResponse())
                    {
                        Stream xmlStream = response.GetResponseStream();
                        xmlDocument.Load(xmlStream);
                        xmlStream.Close();
                        response.Close();
                    }
                }
                else
                {
                    xmlDocument.Load(path);
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("XMLConnector", "GetXMLDocument", path, exception);
            }

            return xmlDocument;
        }

        public XPathDocument GetXPathDocument(string path)
        {
            XPathDocument xpathdocument = null;

            try
            {
                if (path.StartsWith("http"))
                {
                    HttpWebRequest request = new HTTPConnector().BuildWebRequest(path, "text/xml");
                    using (WebResponse response = request.GetResponse())
                    {
                        Stream xmlStream = response.GetResponseStream();
                        xpathdocument = GetXPathDocument(xmlStream);
                        xmlStream.Close();
                        response.Close();
                    }
                }
                else
                {
                    Stream xmlStream = new FileStream(path, FileMode.Open);
                    xpathdocument = GetXPathDocument(xmlStream);
                    xmlStream.Close();
                }
           }
           catch (Exception exception)
           {
               ErrorService.Log("XMLConnector", "GetXPathDocument", path, exception);
            }
            return xpathdocument;
        }

        public XPathDocument GetXPathDocument(Stream xmlStream)
        {
            XmlReader xmlreader = new XmlTextReader(xmlStream);
            XPathDocument xpathdocument = new XPathDocument(xmlreader);
            xmlreader.Close();

            return xpathdocument;
        }

    }
}