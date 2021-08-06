using System;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Schd.Data.Enums;

namespace Schd.Data
{
    public class Notify: IntegrationEvent
    {
        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public NotifyType NotifyType { get; set; }
        
    }
}
