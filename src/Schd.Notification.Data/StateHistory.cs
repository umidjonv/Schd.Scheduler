using System;
using System.ComponentModel.DataAnnotations.Schema;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data
{
    [Table("states_history")]

    public class StateHistory: BaseEntity
    {
        public DateTime Time { get; set; }

        public StateType Type { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

    }
}
