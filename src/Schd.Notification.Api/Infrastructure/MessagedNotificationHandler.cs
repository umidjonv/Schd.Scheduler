using Microsoft.AspNetCore.SignalR;
using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Api.Infrastructure.Hubs;
using Schd.Notification.Data.Enums;
using System;
using System.Threading.Tasks;

namespace Schd.Notification.Api.Infrastructure
{
    public class MessageNotificationHandler: IIntegrationEventHandler<NotifyEvent>
    {
        private readonly IHubContext<MessageHub> messageHub;

        public MessageNotificationHandler(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
        }

        public async Task Handle(NotifyEvent @event, string serviceName)
        {
            await messageHub.Clients.All.SendAsync($"{NotificationType.Message}_{serviceName}", @event);
        }

        public NotificationType GetNotificationType()
        {
            return NotificationType.Message;
        }
    }
}