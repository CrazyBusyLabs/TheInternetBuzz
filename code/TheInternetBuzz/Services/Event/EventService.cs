using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TheInternetBuzz.Data.Event;
using TheInternetBuzz.Providers.Splunk;

namespace TheInternetBuzz.Services.Event
{
    public class EventService
    {
        public static void Log(EventItem eventItem)
        {
            SplunkService.Log(eventItem);
        }
    }
}