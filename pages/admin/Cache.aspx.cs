using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheInternetBuzz.Web.Pages.Admin
{
    public class Cache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DisplayCache()
        {
            Response.Write("<table border=\"1\">");
            foreach (DictionaryEntry entry in HttpRuntime.Cache)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + entry.Key + "</td>");

                if (entry.Value is TheInternetBuzz.Data.List)
                {
                    TheInternetBuzz.Data.List list = (TheInternetBuzz.Data.List)entry.Value;
                    Response.Write("<td>" + entry.Value.GetType() + "(" + list.Count() + ")</td>");
                }
                else
                {
                    Response.Write("<td>" + entry.Value.GetType() + "</td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<hr>");
            Response.Write("Count:" + HttpRuntime.Cache.Count + "<br>");
            Response.Write("Effective Percentage Physical Memory Limit:" + HttpRuntime.Cache.EffectivePercentagePhysicalMemoryLimit + "<br>");
            Response.Write("Effective Private Bytes Limit:" + HttpRuntime.Cache.EffectivePrivateBytesLimit + "<br>");
        }

    }
}
