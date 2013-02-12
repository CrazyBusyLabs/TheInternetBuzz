using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NUnit.Framework;

using TheInternetBuzz.Data.Location;
using TheInternetBuzz.Services.Location;

namespace TheInternetBuzz.Services.Location.Test
{
    [TestFixture]
    public class LocationServiceTest
    {
        [Test]
        public void GetLocalhost()
        {
            LocationService lookupService = new LocationService();
            TheInternetBuzz.Data.Location.Location location = lookupService.GetLocation("127.0.0.1");
            Assert.That(location != null);
            Assert.That(location.city == "Vancouver");
        }

        [Test]
        public void GetShawIP()
        {
            LocationService lookupService = new LocationService();
            TheInternetBuzz.Data.Location.Location location = lookupService.GetLocation("174.7.104.186");
            Assert.That(location != null);
            Console.WriteLine(location.city);
            Assert.That(location.city == "North Vancouver");
        }
    }
}