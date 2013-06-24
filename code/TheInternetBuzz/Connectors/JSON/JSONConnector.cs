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

        public JObject GetJSONObjectWithHeader(string url, string headerName, string headerVaue)
        {
            JObject jsonObject = null;

            string json = new HTTPConnector().GetResponseWithHeader(url, headerName, headerVaue, "application/json");
            jsonObject = JObject.Parse(json);

            return jsonObject;
        }

        public JArray GetJSONArrayWithHeader(string url, string headerName, string headerVaue)
        {
            JArray jsonArray = null;

            string json = new HTTPConnector().GetResponseWithHeader(url, headerName, headerVaue, "application/json");
            jsonArray = JArray.Parse(json);

            return jsonArray;
        }

        public JObject PostJSONObjectWithHeader(string url, string headerName, string headeValue, string data)
        {
            JObject jsonObject = null;

            string json = new HTTPConnector().PostDataWithHeader(url, headerName, headeValue, data);
            jsonObject = JObject.Parse(json);
            
            return jsonObject;
        }
   }
}