using System;
using System.Web;

using log4net;
using log4net.Config;

using TheInternetBuzz.Services.Audit;
using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Services.Logging
{
    public static class LogService
    {        
        public static void Initialize()
        {
            AuditServiceItem auditServiceItem = AuditService.Register("LogService", "Initialize", "");
            AuditService.Start(auditServiceItem);
            try
            {
                LogService.Info(typeof(LogService), "Initialized log4net");
            }
            catch (Exception exception)
            {
                ErrorService.Log("LogService", "Initialize", "", exception);
            }
            AuditService.End(auditServiceItem);
        }

        public static void Debug(Type type, string statement)
        {
            ILog log = LogManager.GetLogger(type);
            log.Debug(statement);
        }

        public static void Warn(Type type, string statement)
        {
            ILog log = LogManager.GetLogger(type);
            log.Warn(statement);
        }

        public static void Info(Type type, string statement)
        {
            ILog log = LogManager.GetLogger(type);
            log.Info(statement);
        }

        public static void Error(Type type, string statement, Exception exception)
        {
            ILog log = LogManager.GetLogger(type);
            log.Error(statement, exception);
        }

        public static void Debug(string type, string statement)
        {
            ILog log = LogManager.GetLogger(type);
            log.Debug(statement);
        }

        public static void Warn(string type, string statement)
        {
            ILog log = LogManager.GetLogger(type);
            log.Warn(statement);
        }

        public static void Info(string type, string statement)
        {
            ILog log = LogManager.GetLogger(type);
            log.Info(statement);
        }

        public static void Error(string type, string statement, Exception exception)
        {
            ILog log = LogManager.GetLogger(type);
            log.Error(statement, exception);
        }
    }
}