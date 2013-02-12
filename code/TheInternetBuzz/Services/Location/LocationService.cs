/**
 * First version:
 * SourceCode: http://www.maxmind.com/app/csharp
 * Database: http://geolite.maxmind.com/download/geoip/database/GeoLiteCity.dat.gz
 */

using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Data.Location;
using TheInternetBuzz.Providers.Maxmind;

namespace TheInternetBuzz.Services.Location
{
    public class LocationService 
    {
        private static LookupService lookupService = Initialize();

        static LookupService Initialize() 
        {
            string databasePath;
            if (HttpContext.Current == null)
            {
                databasePath = AppDomain.CurrentDomain.BaseDirectory + "data/GeoLiteCity.dat";
            }
            else
            {
                databasePath = HttpContext.Current.Server.MapPath("~/data/GeoLiteCity.dat");
            }
            LogService.Info(typeof(TheInternetBuzz.Services.Location.LocationService), "Location Database Initialized with " + databasePath);
            lookupService = new LookupService(databasePath, LookupService.GEOIP_STANDARD);
            return lookupService;
        }

        public TheInternetBuzz.Data.Location.Location GetLocation() 
        {
            string IP = GetIPAddress();
            return GetLocation(IP);
        }

        public TheInternetBuzz.Data.Location.Location GetLocation(string IP)
        {
            TheInternetBuzz.Data.Location.Location location = new TheInternetBuzz.Data.Location.Location();

            if (IP == null || "127.0.0.1".Equals(IP) || "::1".Equals(IP))
            {
                location.city = "Vancouver";
                location.countryName = "Canada";
                location.regionName = "BC";
                location.latitude = 49.25;
                location.longitude = -123.13;
            }
            else
            {
                try
                {
                    location = lookupService.getLocation(IP);
                }
                catch (Exception e)
                {
                    ErrorService.Log("LocationService", "getLocation", IP, e);
                }
            }
            return location;
        }

        private string GetIPAddress() 
        {
            if (HttpContext.Current == null) {
                throw new InvalidOperationException("This method can only be used from within an ASP.NET web request.");
            }
            HttpRequest Request = HttpContext.Current.Request;
            string IP = null;
            if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"])) {
                IP = Request.ServerVariables["REMOTE_ADDR"];
            } else {
                string[] ipRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',');
                IP = ipRange[ipRange.Length - 1].Trim();
            }

            if ("unknown".Equals(IP))
            {
                IP = null;
                LogService.Warn(typeof(LocationService), "Invalid IP address REMOTE_ADDR=" + Request.ServerVariables["REMOTE_ADDR"] + ",=HTTP_X_FORWARDED_FOR" + Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            }
            return IP;
        }
    }
}