using System;

using TheInternetBuzz.Data.Error;
using TheInternetBuzz.Data.Event;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Event;

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
            EventService.Log(new EventItem(SourceEnum.Error, SourceTypeEnum.generic_multiple_line, errorItem.ToString() + " " + exception.StackTrace));

        }

        public static ErrorList GetErrorList()
        {
            return errorList;
        }
    }
}