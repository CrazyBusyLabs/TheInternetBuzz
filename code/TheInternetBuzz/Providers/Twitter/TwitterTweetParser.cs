using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace TheInternetBuzz.Providers.Twitter
{
    public static class TwitterTweetParser
    {
        public static string Link(this string s, string url)
        {
            return string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", url, s);
        }
        public static string ParseTwitterURL(this string s)
        {
            return Regex.Replace(s, @"(http(s)?://)?([\w-]+\.)+[\w-]+(/\S\w[\w- ;,./?%&=]\S*)?", new MatchEvaluator(TwitterTweetParser.URL));
        }
        public static string ParseTwitterUsername(this string s)
        {
            return Regex.Replace(s, "(@)((?:[A-Za-z0-9-_]*))", new MatchEvaluator(TwitterTweetParser.Username));
        }
        public static string ParseTwitterHashtag(this string s)
        {
            return Regex.Replace(s, "(#)((?:[A-Za-z0-9-_]*))", new MatchEvaluator(TwitterTweetParser.Hashtag));
        }
        private static string Hashtag(Match m)
        {
            string x = m.ToString();
            string tag = x.Replace("#", "%23");
            return x.Link("http://search.twitter.com/search?q=" + tag);
        }
        private static string Username(Match m)
        {
            string x = m.ToString();
            string username = x.Replace("@", "");
            return x.Link("http://www.twitter.com/" + username);
        }
        private static string URL(Match m)
        {
            string x = m.ToString();
            return x.Link(x);
        }
    }
}