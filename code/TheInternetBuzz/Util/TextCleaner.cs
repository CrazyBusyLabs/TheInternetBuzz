using System;
using System.Web;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace TheInternetBuzz.Util
{
    static public class TextCleaner
    {
        static public string RemoveHtml(string source)
        {
            string target = source;
            if (target != null)
            {
                target = target.Replace("<b>", "").Replace("</b>", "").Replace("&lt;b&gt;", "").Replace("&lt;/b&gt;", "");
                target = target.Replace("<em>", "").Replace("</em>", "").Replace("&lt;em&gt;", "").Replace("&lt;/em&gt;", "");
                target = HttpUtility.HtmlDecode(target);
                target = target.Replace("<", "&lt;");
                target = target.Replace(">", "&gt;");
            }
            return target;
        }

        static public string StripTag(string text, string openingTag, string closingTag)
        {
            string strippedText = text;

            int startPos = strippedText.IndexOf(openingTag);
            int endPos = strippedText.IndexOf(closingTag);
            string searchText = null;

            while (endPos > startPos && startPos >= 0)
            {
                searchText = strippedText.Substring(0, endPos);
                startPos = searchText.LastIndexOf(openingTag);

                strippedText = strippedText.Substring(0, startPos)
                    + strippedText.Substring(endPos + closingTag.Length);

                startPos = strippedText.IndexOf(openingTag);
                endPos = strippedText.IndexOf(closingTag);
            }

            return strippedText;
        }

        static public string ReplaceLineFeeds(string text)
        {
            string replacedText = text;
            string lineFeed = "\r\n";
            while (replacedText.StartsWith(lineFeed))
            {
                replacedText = replacedText.Substring(lineFeed.Length);
            }
            while (replacedText.EndsWith(lineFeed))
            {
                replacedText = replacedText.Substring(0, replacedText.Length - lineFeed.Length);
            }
            replacedText = replacedText.Replace(lineFeed, "<br/>");

            return replacedText;
        }

        static public string CleanID(string id)
        {
            // (Aa-Zz, 0-9), hyphens (-), percent (%), period (.), and underscores (_)
            return Regex.Replace(id, @"[^A-Za-z0-9_.%-]", "");
        }

        static public string CleanTitle(string title)
        {
            string newTitle = title;

            newTitle = newTitle.Replace("'", "’");
            newTitle = newTitle.Replace("™", "");
            newTitle = newTitle.Replace(".", "");
            newTitle = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newTitle);
            newTitle = newTitle.Replace("’S ", "’s ");
            newTitle = newTitle.Replace(" S ", " s ");
            newTitle = newTitle.Replace("Iphone", "iPhone");
 
            if (newTitle.Contains("\\u") || newTitle.Contains("\\U"))
            {
                Regex rx = new Regex(@"\\[uU]([0-9A-z]{4})");
                newTitle = rx.Replace(newTitle, delegate(Match match)
                {
                    return ((char)Int32.Parse(match.Value.Substring(2), NumberStyles.HexNumber)).ToString();
                });
            }

            return newTitle;
        }

        static public string CleanQuery(string query)
        {
            // (Aa-Zz, 0-9) and underscores (_)
            return Regex.Replace(query, @"[^A-Za-z0-9_]", "");
        }
    }
}