using System;
using TheInternetBuzz.Services.Config;

namespace TheInternetBuzz
{
    public class Version
    {
        static public string Major
        {
            get
            {
                return ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_VERSION_MAJOR, "1");
            }
        }
    }
}
