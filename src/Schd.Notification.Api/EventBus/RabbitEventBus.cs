using Schd.Notification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using Schd.Notification.Api.EventBus;
using Schd.Notification.Api.EventBus.Consts;
using Schd.Notification.Api.EventBus.Providers;
using Schd.Notification.Data.Domain;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.EventBus
{
    
    public class RabbitEventBus : IRabbitEventBus<NotifyEvent>, IConsumer<NotifyEvent>, IPublisher<NotifyEvent>
    {
        private readonly IRabbitProvider _provider;
        
        public RabbitEventBus(IRabbitProvider provider)
        {
            _provider = provider;
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

        public void Consume(string clientId, NotificationType type)
        {
            var consumer = _provider.Consume($"queue_{type}_{clientId}");
            
            //consumer. += async (data, eventArgs) =>
            //{
            //    var body = eventArgs.Body.ToArray();

            //    await Task.Yield();
            //};

        }

        public void Publish(NotifyEvent model)
        {
            _provider.Publish(model.Type.GetDisplayName(), model, model.Type.GetDisplayName());
            
        }

        public void Consume(NotifyEvent model)
        {
            
            
        }
    }
}
