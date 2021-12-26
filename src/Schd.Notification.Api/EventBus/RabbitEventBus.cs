using Schd.Notification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using Schd.Notification.Api.EventBus;
using Schd.Notification.Api.EventBus.Consts;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Api.EventBus.Providers;
using Schd.Notification.Data.Enums;
using Schd.Notification.Api.EventBus.Abstractions;
using RabbitMQ.Client.Events;
using Schd.Common.Helpers;
using RabbitMQ.Client;

namespace Schd.Notification.EventBus
{
    
    public class RabbitEventBus : IRabbitEventBus<NotifyEvent>
    {
        private readonly RabbitProvider _provider;
        private readonly EventBusManager _eventBusManager;

        public RabbitEventBus(RabbitProvider provider, EventBusManager eventBusManager)
        {
            _provider = provider;
            _eventBusManager = eventBusManager;
        }
        
        public IConsumer<NotifyEvent> Consumer { get; set; }
        public IPublisher<NotifyEvent> Publisher { get; set; }

        private void CreateChannel(NotificationType type)
        {
            _provider.DeclareExchange($"exchange_{type.GetDisplayName()}");
            //_provider.Bind(Constants.LOG_QUEUE, NotificationType.Log.GetDisplayName());
        }

        
        public void CreateQueue(string clientId, NotificationType type)
        {
            var queueName = $"queue_{type}_{clientId}";
            var route = $"{clientId}_{type}";
            var exchange = $"exchange_{type.GetDisplayName()}";
            _provider.QueueDeclare(queueName);
            _provider.Bind(exchange, queueName, route);
        }

        public void Consume<T, TH>(string serviceName) where T : INotifyEvent
            where TH : IIntegrationEventHandler<T>
        {
            var consumer = new AsyncEventingBasicConsumer(_provider.Channel);

            consumer.Received += async (data, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();

                var @event = body.Deserialize<INotifyEvent>();

                if (_eventBusManager.HasSubscribed(serviceName))
                {
                    var handler = _eventBusManager.GetSubscriber(serviceName, typeof(TH));

                    await handler.Handle(@event, serviceName);
                    
                }


            };

            _provider.Channel.BasicConsume(serviceName, false, consumer);

        }

        public void Publish(NotifyEvent model)
        {
            _provider.Publish(model.Type.GetDisplayName(), model, model.Type.GetDisplayName());
            
        }
        
    }
}
