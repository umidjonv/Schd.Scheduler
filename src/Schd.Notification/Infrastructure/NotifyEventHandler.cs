using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Schd.Data;

namespace Schd.Notification.Infrastructure
{
    public class NotifyEventHandler : IIntegrationEventHandler<Notify>
    {
        public async Task Handle(Notify @event)
        {
            
        }
    }
}
