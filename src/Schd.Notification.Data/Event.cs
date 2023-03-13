using Schd.Notification.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Schd.Notification.Data
{
    [Table("events")]

    public class Event:BaseEntity
    {
        
        public NotificationType Type { get; set; }

        public StateType State { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual List<State> States { get; set; }
        
        public virtual List<StateHistory> StateHistories { get; set; }
    }
}
