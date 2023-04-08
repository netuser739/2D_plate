using System.Collections.Generic;

namespace Tool.Analytics
{
    internal interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
        void TransactionEvent(string productName, decimal amount, string currency);
    }
}
