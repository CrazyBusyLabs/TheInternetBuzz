using System;
using NUnit.Framework;
using TheInternetBuzz.Util;
using System.Globalization;

namespace TheInternetBuzz.Util.Test    
{
    [TestFixture]
    public class DateParserTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseNullArguments()
        {
            DateParser.Parse(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseEmptyDateArgument()
        {
            DateParser.Parse("", null);
        }

        [Test]
        public void ParseDate()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime.ParseExact("2013-01-22T11:56:42-07:0 +0", "yyyy'-'MM'-'dd'T'HH':'mm':'ss z", cultureInfo);
        }
    }
}
