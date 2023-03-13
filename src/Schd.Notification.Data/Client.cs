using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Schd.Notification.Data
{
    [Table("clients")]

    public class Client:BaseEntity
    {
        public string Secret { get; set; }

        public string Name { get; set; }

        public bool IsApproved { get; set; }

        public string WebUrl { get; set; }

        public virtual List<State> States { get; set; } 

        public virtual List<StateHistory> StateHistories { get; set; } 

        public virtual List<Event> Events { get; set; } 
    }
}
