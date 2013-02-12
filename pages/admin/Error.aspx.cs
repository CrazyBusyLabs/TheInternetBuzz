using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Data.Error;
using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Web.Pages.Admin
{
    public class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DisplayError()
        {
            Response.Write("<table border=\"1\">");
            ErrorList errorList = ErrorService.GetErrorList();

            if (errorList != null)
            {
                foreach (ErrorItem errorItem in errorList)
                {
                    Response.Write("<tr>");
                    Response.Write("<td>" + errorItem.Service + "</td>");
                    Response.Write("<td>" + errorItem.Action + "</td>");
                    Response.Write("<td>" + errorItem.Label + "</td>");
                    Response.Write("<td>" + errorItem.ErrorException + "</td>");
                    Response.Write("<td>" + errorItem.ErrorDateTime.ToUniversalTime().ToString("r") + "</td>");
                    Response.Write("</tr>");
                }
            }
            Response.Write("</table>");
            Response.Write("<hr>");
            if (errorList != null)
            {
                Response.Write("Count:" + errorList.Count() + "<br>");
            }
            Response.Write("Effective Percentage Physical Memory Limit:" + HttpRuntime.Cache.EffectivePercentagePhysicalMemoryLimit + "<br>");
            Response.Write("Effective Private Bytes Limit:" + HttpRuntime.Cache.EffectivePrivateBytesLimit + "<br>");
        }
    }
}