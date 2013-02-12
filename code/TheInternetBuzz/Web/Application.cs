using System;
using System.Reflection;
using System.Web;

using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Trends;

namespace TheInternetBuzz.Web
{
    public class Application
    {
        public static readonly bool IsDebugMode =     
#if DEBUG          
            true; // Debug Build is selected     
#else          
            false; // Release or other Build is selected     
#endif   
        public static readonly bool IsReleaseMode = !IsDebugMode;


        public static void Start()
        {
            LogService.Initialize();
            LogService.Info(typeof(TheInternetBuzz.Web.Application), "Application Started");

            TrendsBuilder.StartTimer();
        }

        public static void End()
        {
            TrendsBuilder.EndTimer();

            HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
            LogService.Info(typeof(TheInternetBuzz.Web.Application), "Application End " + shutDownMessage);
        }
    }
}