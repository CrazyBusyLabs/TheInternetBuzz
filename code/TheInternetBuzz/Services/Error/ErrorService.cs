using System;

using TheInternetBuzz.Data.Error;
using TheInternetBuzz.Services.Logging;

namespace TheInternetBuzz.Services.Error
{
    public static class ErrorService
    {
        private static ErrorList errorList = new ErrorList();
        private static Object guard = new Object();

        public static void Log(string category, string action, string label, Exception exception)
        {
            ErrorItem errorItem = new ErrorItem(category, action, label, exception);

            lock (guard)
            {
                errorList.Add(errorItem);
            }

            LogService.Error("TheInternetBuzz.Error", errorItem.ToString(), exception);
        }

        public static ErrorList GetErrorList()
        {
            return errorList;
        }
    }
}