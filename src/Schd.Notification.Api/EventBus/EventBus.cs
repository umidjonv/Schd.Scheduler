using MassTransit;
using Schd.Notification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schd.Notification.EventBus
{
    public class EventBus : IEventBus<Notify>, IConsumer<Notify>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        
        public IConsumer<Notify> Consumer { get; set; }

        public async Task Consume(ConsumeContext<Notify> context)
        {
            //_logger.LogInformation("Value: {Value}", context.Message);
        }

        public void Publish(Notify model)
        {
            _publishEndpoint.Publish<Notify>(model);
        }
    }
}
