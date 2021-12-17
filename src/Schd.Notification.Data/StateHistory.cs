using System;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data
{
    public class StateHistory: BaseEntity
    {
        public DateTime Time { get; set; }

        public StateType Type { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public Guid NotifyId { get; set; }

        public virtual Notify Notify { get; set; }

    }
}
