using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Data.Enums;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Schd.Notification.Api.Infrastructure.Hubs;

namespace Schd.Notification.Api.Infrastructure
{
    public class CommandNotificationHandler: IIntegrationEventHandler<NotifyEvent>
    {
        private readonly IHubContext<CommandHub> _commandHub;

        public CommandNotificationHandler(IHubContext<CommandHub> commandHub)
        {
            _commandHub = commandHub;
        }

        public NotificationType GetNotificationType()
        {
            return NotificationType.Command;
        }

        public async Task Handle(NotifyEvent @event, string serviceName)
        {
            await _commandHub.Clients.All.SendAsync($"{NotificationType.Command}_{serviceName}", @event.Message);
        }

        
    }
}