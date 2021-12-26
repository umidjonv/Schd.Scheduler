using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using System;
using System.Threading.Tasks;
using Schd.Notification.Api.Infrastructure.Hubs;
using Schd.Notification.Data.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Schd.Notification.Api.Infrastructure
{
    public class LogNotificationHandler: IIntegrationEventHandler<NotifyEvent>
    {
        private readonly IHubContext<LogHub> logHub;

        public LogNotificationHandler(IHubContext<LogHub> logHub)
        {
            this.logHub = logHub;
        }

        public NotificationType GetNotificationType()
        {
            throw new NotImplementedException();
        }

        public async Task Handle(NotifyEvent @event, string serviceName)
        {
            await logHub.Clients.All.SendAsync($"{NotificationType.Log}_{serviceName}", @event); 
        }

    }
}
