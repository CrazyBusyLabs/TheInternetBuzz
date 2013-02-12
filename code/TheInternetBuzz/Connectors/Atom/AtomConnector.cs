using System;
using System.Net;
using System.IO;
using System.Xml;

using TheInternetBuzz.Util;

namespace TheInternetBuzz.Connectors.Atom {

    public class AtomConnector {

        public EntryData GetEntryData(string url) 
        {

            WebRequest request = WebRequest.Create(url);
            XmlDocument atomDoc = null;

            using (WebResponse response = request.GetResponse())
            {
                Stream atomStream = response.GetResponseStream();
                
                atomDoc = new XmlDocument();
                atomDoc.Load(atomStream);

                atomStream.Close();
                response.Close();
            }

            XmlNamespaceManager nsmanager = new XmlNamespaceManager(atomDoc.NameTable);
            nsmanager.AddNamespace("default", "http://www.w3.org/2005/Atom");

            EntryData entryData = new EntryData();

            EntryList entryList = new EntryList();
            entryData.EntryList = entryList;

            XmlNode updatedNode = atomDoc.SelectSingleNode("default:feed/default:updated", nsmanager);
            if (updatedNode != null)
            {
                // format 2009-12-24T05:53:20Z
                // format 2013-01-23T11:51:59-07:0 +0
                string dateString = updatedNode.InnerText;
                if (dateString.Contains("-07:0"))
                {
                    dateString = dateString.Replace("-07:0", " -7");
                }
                else
                {
                    dateString = dateString.Substring(0, dateString.Length - 1) + " +0";
                }
                DateTime updatedDate = DateParser.Parse(dateString, "yyyy'-'MM'-'dd'T'HH':'mm':'ss z");
                entryData.LastBuildDate = updatedDate;
            }

            XmlNodeList atomEntryNodeList = atomDoc.SelectNodes("default:feed/default:entry", nsmanager);

            if (atomEntryNodeList != null)
            {
                Console.WriteLine("LIST " + atomEntryNodeList.Count);

                foreach (XmlNode atomEntryNode in atomEntryNodeList)
                {
                    Entry entry = new Entry();

                    foreach (XmlNode childNode in atomEntryNode.ChildNodes)
                    {
                        if (childNode.Name == "title")
                        {
                            entry.Title = childNode.InnerText;
                        }
                        else if (childNode.Name == "id")
                        {
                            entry.ID = childNode.InnerText;
                        }
                        else if (childNode.Name == "content")
                        {
                            entry.Content = childNode.InnerText;
                        }
                        else
                        {
                            entry.Set(childNode.Name, childNode.InnerText);
                        }
                    }
                    entryList.Add(entry);
                }
            }
            return entryData;
        }
    }
}