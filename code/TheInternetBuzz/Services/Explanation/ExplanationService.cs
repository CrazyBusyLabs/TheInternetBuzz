using System;

using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Data.Explanation;

using TheInternetBuzz.Services.Audit;
using TheInternetBuzz.Services.Error;

using TheInternetBuzz.Providers.WhatTheTrend;

namespace TheInternetBuzz.Services.Explanation
{
    public class ExplanationService
    {

        public ExplanationService()
        {
        }

        public ExplanationItem GetExplanation(string query)
        {
            AuditServiceItem auditServiceItem = AuditService.Register("ExplanationService", "GetExplanation", query);
            AuditService.Start(auditServiceItem);

            ExplanationItem explanationItem = ExplanationCacheHelper.ReadExplanationItem(query);
            if (explanationItem == null)
            {
                try
                {
                    WhatTheTrendExplanationService WhatTheTrendExplanationService = new WhatTheTrendExplanationService();
                    explanationItem = WhatTheTrendExplanationService.GetExplanation(query);
                    if (explanationItem != null)
                    {
                        ExplanationCacheHelper.CacheExplanationItem(query, explanationItem);
                    }
                }
                catch (Exception exception)
                {
                    ErrorService.Log("ExplanationService", "GetExplanation", query, exception);
                }
            }

            AuditService.End(auditServiceItem);

            return explanationItem;
        }

    }
}
