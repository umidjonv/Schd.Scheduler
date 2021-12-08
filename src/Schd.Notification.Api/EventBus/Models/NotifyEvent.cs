using System;
using System.Collections.Generic;
using System.Text;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data.Domain
{
    public class NotifyEvent
    {
        public Guid Id { get; set; } = new Guid();

        public Guid NotifyId { get; set; }

        public NotificationType Type { get; set; }

        public StateType State { get; set; }

        public INotify Notify { get; set; }

    }
}
