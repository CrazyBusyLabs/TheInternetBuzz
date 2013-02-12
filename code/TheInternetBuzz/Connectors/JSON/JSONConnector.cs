using System;
using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Connectors.HTTP;

namespace TheInternetBuzz.Connectors.JSON
{
    public class JSONConnector 
    {
        public JObject GetJSONObject(string url)
        {
            JObject jsonObject = null;

            string json = new HTTPConnector().GetResponse(url, "application/json");
            jsonObject = JObject.Parse(json);
            
            return jsonObject;
        }

        public JArray GetJSONArray(string url)
        {
            JArray jsonArray = null;
            string json = new HTTPConnector().GetResponse(url, "application/json");
            jsonArray = JArray.Parse(json);

            return jsonArray;
        }
   }
}