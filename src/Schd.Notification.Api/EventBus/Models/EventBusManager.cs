using Schd.Notification.Api.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Schd.Notification.Api.EventBus.Models
{
    public class EventBusManager
    {


        private Dictionary<string, List<IIntegrationEventHandler>> _subscribers;
        private readonly IServiceCollection _serviceProvider;

        public EventBusManager(IServiceCollection serviceProvider)
        {
            _subscribers = new Dictionary<string, List<IIntegrationEventHandler>>();
            _serviceProvider = serviceProvider;
        }


        public void SubscribeHandler(string serviceName, IIntegrationEventHandler handler) 
        {
            if (_subscribers.ContainsKey(serviceName))
            {
                var containType = false;
                foreach (IIntegrationEventHandler registeredHandler in _subscribers[serviceName])
                {
                    if(registeredHandler.GetType() == handler.GetType())
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

        public IIntegrationEventHandler<INotifyEvent> GetSubscriber(string serviceName, Type handlerType) 
        {
            if (_subscribers.ContainsKey(serviceName))
            {
                foreach (IIntegrationEventHandler<INotifyEvent> registeredHandler in _subscribers[serviceName])
                {
                    if (registeredHandler.GetType() == handlerType)
                        return registeredHandler;
                }
            }

            return null;
        }
    }
}
