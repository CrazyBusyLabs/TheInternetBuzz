using System;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Providers.Wikipedia
{
    public class WikipediaTopicParser
    {
        public WikipediaTopicParser()
        {
        }

        public string Parse(string wikiText)
        {
            string strippedWikiText = null;

            if (!(wikiText.Contains("may refer to")))
            {
                strippedWikiText = wikiText;

                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "&lt;ref", "&lt;/ref&gt;"); // remove ref
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "&lt;ref", "/&gt;"); // remove ref (in one tag)
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "{{", "}}"); // remove boxes
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "&lt;!--", "--&gt;"); // remove comments
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "&lt;", "&gt;"); // remove any other tags
                strippedWikiText = StripWikipediaLinks(strippedWikiText); // remove links but keep text
                strippedWikiText = strippedWikiText.Replace("'''", ""); // remove bold
                strippedWikiText = strippedWikiText.Replace("''", ""); // remove italic
                strippedWikiText = strippedWikiText.Replace("__TOC__", ""); // remove TOC
                strippedWikiText = strippedWikiText.Replace("__", ""); // remove underline
                strippedWikiText = strippedWikiText.Replace("&amp;nbsp;", "&nbsp;"); // remove underline
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "{", "}"); // remove {}
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "(", ")"); // remove ()
                strippedWikiText = TextCleaner.StripTag(strippedWikiText, "[", "]"); // strip links with one [
                strippedWikiText = TextCleaner.ReplaceLineFeeds(strippedWikiText);
                strippedWikiText = strippedWikiText.Replace("<br/><br/><br/><br/>", "");
                strippedWikiText = strippedWikiText.Replace("<br/><br/> <br/><br/>", "");
                strippedWikiText = strippedWikiText.Replace(" ,", ",");
                strippedWikiText = strippedWikiText.Trim();
                while (strippedWikiText.StartsWith("<br/>"))
                {
                    strippedWikiText = strippedWikiText.Substring("<br/>".Length);
                }
            }
            return strippedWikiText;
        }

        private string StripWikipediaLinks(string text)
        {
            string strippedText = text;

            string startTag = "[[";
            string middleTag = "|";
            string endTag = "]]";

            int endPos = strippedText.IndexOf(endTag);
            string searchText = null;
            int startPos = -1;
            int middlePos = -1;

            if (endPos >= 0)
            {
                searchText = strippedText.Substring(0, endPos);
                startPos = searchText.LastIndexOf(startTag);
                middlePos = searchText.LastIndexOf(middleTag);
            }

            while (endPos > startPos && startPos >= 0)
            {
                if (middlePos > startPos)
                {
                    strippedText = strippedText.Substring(0, startPos)
                        + strippedText.Substring(middlePos + middleTag.Length, endPos - middleTag.Length - middlePos)
                        + strippedText.Substring(endPos + endTag.Length);
                }
                else
                {
                    strippedText = strippedText.Substring(0, startPos)
                        + strippedText.Substring(startPos + startTag.Length, endPos - startTag.Length - startPos)
                        + strippedText.Substring(endPos + endTag.Length);
                }

                endPos = strippedText.IndexOf(endTag);
                if (endPos >= 0)
                {
                    searchText = strippedText.Substring(0, endPos);
                    startPos = searchText.LastIndexOf(startTag);
                    middlePos = searchText.LastIndexOf(middleTag);
                }
            }

            return strippedText;
        }
    }
}