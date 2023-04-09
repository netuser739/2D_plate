using System.Collections.Generic;
using Services.Analytics;

namespace Tool.Analytics.UnityAnalytics
{
    internal class Dev2DevAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName)
        {
            throw new System.NotImplementedException();
        }

        public void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            throw new System.NotImplementedException();
        }

        public void TransactionEvent(string productName, decimal amount, string currency)
        {
            throw new System.NotImplementedException();
        }
    }
}
