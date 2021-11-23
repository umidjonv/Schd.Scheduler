using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Schd.Notification.Data;
using Schd.Notification.Data.Domain;

namespace Schd.Notification.Models.EventBus
{
    public class NotifyConsumer : IConsumer<NotifyEvent>
    {
        ILogger<NotifyConsumer> _logger;

        public NotifyConsumer(ILogger<NotifyConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<NotifyEvent> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message);
        }
        
    }
}
