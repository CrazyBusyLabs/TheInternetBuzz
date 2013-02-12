using System;
using System.Configuration;

using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Services.Config
{
    public static class ConfigService
    {
        public static int GetConfig(string key, int defaultValue)
        {
            int value = 0;
            try
            {
                string configValue = ConfigurationManager.AppSettings[key];
                if (configValue != null && configValue.Length > 0)
                {
                    value = int.Parse(configValue);
                }
                else
                {
                    value = defaultValue;
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("ConfigService", "GetConfig", key, exception);
                value = defaultValue;
            }

            return value;
        }

        public static string GetConfig(string key, string defaultValue)
        {
            string value = "";
            try
            {
                string configValue = ConfigurationManager.AppSettings[key];
                if (configValue != null && configValue.Length > 0)
                {
                    value = configValue;
                }
                else
                {
                    value = defaultValue;
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("ConfigService", "GetConfig", key, exception);
                value = defaultValue;
            }

            return value;
        }

        public static bool GetConfig(string key, bool defaultValue)
        {
            bool value = false;
            try
            {
                string configValue = ConfigurationManager.AppSettings[key];
                if (configValue != null && configValue.Length > 0)
                {
                    value = bool.Parse(configValue);
                }
                else
                {
                    value = defaultValue;
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("ConfigService", "GetConfig", key, exception);
                value = defaultValue;
            }

            return value;
        }
    }
}
