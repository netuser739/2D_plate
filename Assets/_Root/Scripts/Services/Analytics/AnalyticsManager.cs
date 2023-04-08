using System.Collections.Generic;
using Tool.Analytics.UnityAnalytics;
using UnityEngine;

namespace Tool.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService(),
            };
        }

        public void SendMainMenuOpenedEvent() =>
            SendEvent("MainMenuOpened");

        public void SendGameStartedEvent()
        {
            SendEvent("GameStarted");
            Debug.Log("GameStarted");
        }

        public void TransactionProd2Event()
        {
            TransactionEvent("prod_2", 1.2m, "USD");
            Debug.Log("prod_2, 1.2m, USD");
        }

        private void SendEvent(string eventName)
        {
            foreach(IAnalyticsService service in _services)
                service.SendEvent(eventName);
        }

        private void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEvent(eventName, eventData);
        }

        private void TransactionEvent(string eventName, decimal amount, string currency)
        {
            foreach(IAnalyticsService service in _services)
                service.TransactionEvent(eventName, amount, currency);
        }
    }
}
