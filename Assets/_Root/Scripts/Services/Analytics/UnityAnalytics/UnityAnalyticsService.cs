using System.Collections.Generic;

namespace Tool.Analytics.UnityAnalytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);

        public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);

        public void TransactionEvent(string productName, decimal amount, string currency) =>
            UnityEngine.Analytics.Analytics.Transaction(productName, amount, currency);
       
    }
}