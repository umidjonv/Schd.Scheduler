using System;
using System.Collections.Generic;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data
{
    public class State: BaseEntity
    {
        public DateTime Time { get; set; }

        public StateType Type { get; set; }

        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        public Guid NotifyId { get; set; }

        public Notify Notify { get; set; }

        public virtual List<Notify> Notifies { get; set; }

    }
}
