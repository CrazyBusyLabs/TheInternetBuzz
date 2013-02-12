using System;
using System.Collections;
using System.Globalization;

using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Util
{
    static public class DateParser
    {

        static public DateTime Parse(string dateString, string format)
        {
            DateTime result = DateTime.Now;
            try
            {
                CultureInfo cultureInfo = new CultureInfo("en-US");
                result = DateTime.ParseExact(dateString, format, cultureInfo);
            }
            catch (FormatException exception)
            {
                ErrorService.Log("DateParser", "Parse", dateString + "|" + format, exception);
            }

            return result;
        }
    }
}