using System;
using System.Web;
using System.Web.Caching;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Services.Cache
{
    public class CacheCallback
    {
        public void RemovedCallback(String cacheName, Object obj, CacheItemRemovedReason reason)
        {
            LogService.Debug(typeof(TheInternetBuzz.Services.Cache.CacheCallback), "Remove cache name:" + cacheName + " for reason:" + reason + "(Cache Count:" + HttpRuntime.Cache.Count + ")");
        }

    }
}
