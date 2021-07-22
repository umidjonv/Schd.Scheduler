using System;
using System.Collections.Generic;
using System.Text;
using Schd.EventBus.Abstractions;

namespace Schd.EventBus
{
    public class RabbitEventBus:IEventBus, IDisposable
    {
        private readonly IRabbitPersistentConnection _persistentConnection;

        public RabbitEventBus(IRabbitPersistentConnection persistentConnection, string queueName = null,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
        }

        public void Publish(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
