using System;

namespace Schd.Notification.Data
{
    public class StateHistory: BaseEntity
    {
        public DateTime Time { get; set; }

        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        public Guid NotifyId { get; set; }

        public Notify Notify { get; set; }

    }
}
