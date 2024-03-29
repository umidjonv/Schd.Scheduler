﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Schd.Notification.Models.EventBus
{
    public class NotifyConsumer : IConsumer<NotifyMessage>
    {
        ILogger<NotifyConsumer> _logger;

        public NotifyConsumer(ILogger<NotifyConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<NotifyMessage> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message);
        }
        
    }
}
