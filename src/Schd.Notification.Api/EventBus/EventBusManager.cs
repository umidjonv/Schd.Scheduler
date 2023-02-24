using Schd.Notification.Api.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Schd.Notification.Api.EventBus.Models;

namespace Schd.Notification.Api.EventBus
{
    public class EventBusManager
    {


        private Dictionary<string, List<IIntegrationEventHandler>> _subscribers;


        public EventBusManager()
        {
            _subscribers = new Dictionary<string, List<IIntegrationEventHandler>>();

        }


        public void SubscribeHandler(string serviceName, IIntegrationEventHandler handler)
        {
            if (_subscribers.ContainsKey(serviceName))
            {
                var containType = false;
                foreach (IIntegrationEventHandler registeredHandler in _subscribers[serviceName])
                {
                    if (registeredHandler.GetType() == handler.GetType())
                        containType = true;
                }

                if (!containType)
                {
                    _subscribers[serviceName].Add(handler);
                }
            }
            else
            {
                _subscribers.Add(serviceName, new List<IIntegrationEventHandler>{
                    handler
                });
            }

        }

        public void UnsubscribeService(string serviceName)
        {
            if (_subscribers.ContainsKey(serviceName))
            {
                _subscribers.Remove(serviceName);
            }
        }

        public void UnsubscribeHandler(string serviceName, Type handlerType)
        {
            if (_subscribers.ContainsKey(serviceName))
            {
                foreach (IIntegrationEventHandler registeredHandler in _subscribers[serviceName])
                {
                    if (registeredHandler.GetType() == handlerType)
                        _subscribers[serviceName].Remove(registeredHandler);
                }

            }
        }



        public bool HasSubscribed(string serviceName)
        {
            return _subscribers.ContainsKey(serviceName);
        }

        public IIntegrationEventHandler<T> GetSubscriber<T>(string serviceName, Type handlerType) where T :INotifyEvent
        {
            if (_subscribers.ContainsKey(serviceName))
            {
                foreach (IIntegrationEventHandler<T> registeredHandler in _subscribers[serviceName])
                {


                    if (registeredHandler.GetType() == handlerType)
                        return registeredHandler;

                }
            }

            return null;
        }
    }
}
