using System;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data
{
    public class Notify:BaseEntity
    {
        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public NotifyType NotifyType { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public Guid StateId { get; set; }

        public virtual State State { get; set; }

    }
}
