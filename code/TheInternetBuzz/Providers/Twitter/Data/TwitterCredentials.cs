using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheInternetBuzz.Providers.Twitter.Data
{
    public class TwitterCredentials
    {
        public TwitterCredentials(string user, string token)
        {
            User = user;
            Token = token;
        }

        public string User
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }
    }
}

