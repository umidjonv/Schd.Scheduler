using System;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Api.EventBus.Models
{
    public class NotifyEvent:INotifyEvent
    {
        public Guid Id { get; set; } = new Guid();

        public Guid NotifyId { get; set; }

        public NotificationType Type { get; set; }

        public StateType State { get; set; }

    }
}
