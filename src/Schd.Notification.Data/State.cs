using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data
{
    [Table("states")]

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
