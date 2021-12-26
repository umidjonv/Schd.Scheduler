using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Api.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : INotifyEvent
    {
        Task Handle(TIntegrationEvent @event, string serviceName);

        NotificationType GetNotificationType();

    }

    public interface IIntegrationEventHandler
    {
    }

}
