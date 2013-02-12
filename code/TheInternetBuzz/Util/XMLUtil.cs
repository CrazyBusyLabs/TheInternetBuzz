using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Web;

namespace TheInternetBuzz.Util
{
    public static class XMLUtil
    {

        static public string XmlToString(XmlDocument xmlDocument)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDocument.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}