using System;
using System.Web;
using System.Web.Caching;

namespace TheInternetBuzz.Services.Cache
{
    public static class CacheService
    {
        public static void Put(string cacheName, int cacheExpiration, Object obj)
        {
            CacheItemRemovedCallback onRemove = new CacheItemRemovedCallback(new CacheCallback().RemovedCallback);
            HttpRuntime.Cache.Insert(cacheName, obj, null, DateTime.UtcNow.AddMinutes(cacheExpiration), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, onRemove);
        }

        public static Object Read(string cacheName)
        {
            return HttpRuntime.Cache.Get(cacheName);
        }

    }
}
