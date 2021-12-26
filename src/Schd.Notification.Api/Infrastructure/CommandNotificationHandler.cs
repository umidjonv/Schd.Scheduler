using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Data.Enums;
using System;
using System.Threading.Tasks;

namespace Schd.Notification.Api.Infrastructure
{
    public class CommandNotificationHandler: IIntegrationEventHandler<NotifyEvent>
    {
        public CommandNotificationHandler()
        {
        }

        public NotificationType GetNotificationType()
        {
            throw new NotImplementedException();
        }

        public Task Handle(NotifyEvent @event, string serviceName)
        {
            throw new System.NotImplementedException();
        }

        
    }
}