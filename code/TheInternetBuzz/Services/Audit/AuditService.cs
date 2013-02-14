using System;
using System.Web;

using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Data.Event;
using TheInternetBuzz.Services.Event;
using TheInternetBuzz.Services.Logging;

namespace TheInternetBuzz.Services.Audit
{
    public static class AuditService
    {
        private static AuditList auditList = new AuditList();
        private static Object guard = new Object();

        public static AuditServiceItem Register(string service, string action, string label)
        {
            AuditServiceItem auditItem = new AuditServiceItem(service, action, label);
        
            lock (guard) 
            {
                auditList.Add(auditItem);
            }

            return auditItem;
        }

        public static void Start(AuditServiceItem auditItem)
        {
            auditItem.StartTime = DateTime.UtcNow;
        }

        public static void End(AuditServiceItem auditItem)
        {
            auditItem.EndTime = DateTime.UtcNow;
            auditItem.Duration = auditItem.EndTime - auditItem.StartTime;

            LogService.Info(LoggerKeys.THEINTERNETBUZZ_LOGGING_AUDIT, auditItem.ToString());
            EventService.Log(new EventItem(SourceEnum.Audit, SourceTypeEnum.generic_single_line, auditItem.ToString()));
        }

        public static AuditList GetAuditList()
        {
            return auditList;
        }
    }
}