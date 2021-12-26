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

        public Guid EventId { get; set; }

        public virtual Event Event  { get; set; }
        
    }
}
