using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Services.Audit;

namespace TheInternetBuzz.Web.Pages.Admin
{
    public class Audit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DisplayAudit()
        {
            Response.Write("<table border=\"1\">");
            AuditList auditList = AuditService.GetAuditList();

            if (auditList != null)
            {
                foreach (AuditServiceItem auditServiceItem in auditList)
                {
                    Response.Write("<tr>");
                    Response.Write("<td>" + auditServiceItem.Service + "</td>");
                    Response.Write("<td>" + auditServiceItem.Action + "</td>");
                    Response.Write("<td>" + auditServiceItem.Label + "</td>");
                    if (auditServiceItem.StartTime != null)
                    {
                        Response.Write("<td>" + auditServiceItem.StartTime.ToUniversalTime().ToString("r") + "</td>");
                    }
                    else
                    {
                        Response.Write("<td>&nbsp;</td>");
                    }
                    if (auditServiceItem.EndTime != null)
                    {
                        Response.Write("<td>" + auditServiceItem.EndTime.ToUniversalTime().ToString("r") + "</td>");
                    }
                    else
                    {
                        Response.Write("<td>&nbsp;</td>");
                    }
                    if (auditServiceItem.Duration != null)
                    {
                        Response.Write("<td>" + auditServiceItem.Duration.TotalMilliseconds + "</td>");
                    }
                    else
                    {
                        Response.Write("<td>&nbsp;</td>");
                    }
                    Response.Write("</tr>");
                }
            }
            Response.Write("</table>");
            Response.Write("<hr>");
            if (auditList != null)
            {
                Response.Write("Count:" + auditList.Count() + "<br>");
            }
            Response.Write("Effective Percentage Physical Memory Limit:" + HttpRuntime.Cache.EffectivePercentagePhysicalMemoryLimit + "<br>");
            Response.Write("Effective Private Bytes Limit:" + HttpRuntime.Cache.EffectivePrivateBytesLimit + "<br>");
        }

    }
}
