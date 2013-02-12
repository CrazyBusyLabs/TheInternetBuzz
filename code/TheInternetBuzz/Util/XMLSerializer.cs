using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;

namespace TheInternetBuzz.Util
{
    static public class XMLSerializer
    {
        static public void Serialize(object obj, Type type, Type[] extraTypes, string filepath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(type,extraTypes);
                using (StreamWriter streamWriter = new StreamWriter(filepath))
                {
                    serializer.Serialize(streamWriter, obj);
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("XMLSerializer", "Serialize", filepath, exception);
            }
        }

        static public Object Deserialize(string filepath, Type type, Type[] extraTypes)
        {
            Object obj = null;

            if (File.Exists(filepath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(type, extraTypes);
                    using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                    {
                         obj = serializer.Deserialize(fileStream);
                    }

                }
                catch (Exception exception)
                {
                    ErrorService.Log("XMLSerializer", "Deserialize", filepath, exception);
                }
            }

            return obj;
        }
    }
}
