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
using Newtonsoft.Json;
using System.Text;

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

        public void DeclareExchange(NotificationType type) 
        {
            if (_provider.Channel == null)
                _provider.CreateChannel();

            if (_provider.Channel != null)
                _provider.DeclareExchange($"exchange_{type.GetDisplayName()}");
        }


        public void CreateQueue(string serviceName, NotificationType type)
        {
            
            
            var queueName = GetQueueName(serviceName, type);
            var route = GetRouteName(serviceName, type); ;
            var exchange = GetExchangeName(type);
            _provider.QueueDeclare(queueName);
            _provider.Bind(exchange, queueName, route);
        }

        public void Consume<T, TH>(string serviceName) where T : INotifyEvent
            where TH : IIntegrationEventHandler<T>
        {
            var consumer = new AsyncEventingBasicConsumer(_provider.Channel);
            if (_eventBusManager.HasSubscribed(serviceName))
            {
                var handler = _eventBusManager.GetSubscriber<T>(serviceName, typeof(TH));

                consumer.Received += async (data, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var convertibleObject = Encoding.ASCII.GetString(body, 0, body.Length);
                    var @event = JsonConvert.DeserializeObject<T>(convertibleObject);

                    //if(handler)
                    await handler.Handle(@event, serviceName);




                };

                _provider.Channel.BasicConsume(GetQueueName(serviceName, handler.GetNotificationType()), false, consumer);
                
            }

            

        }

        public void Publish(NotifyEvent model)
        {
            _provider.Publish(GetExchangeName(model.Type), model, GetRouteName(model.ServiceId.ToString(), model.Type));

        }

        private string GetQueueName(string serviceName, NotificationType type)
        {
            return $"queue_{type}_{serviceName}";
        }

        private string GetRouteName(string serviceName, NotificationType type) => $"{serviceName}_{type}";

        private string GetExchangeName(NotificationType type) => $"exchange_{type}";
        private NotificationType GetNotificationType(IIntegrationEventHandler<INotifyEvent> handler)
        {
            return handler.GetNotificationType();
        }

    }
}
