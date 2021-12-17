using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Models
{
    public class CommandModel
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public MessageType Type { get; set; }

        public Guid ClientId { get; set; }
        
    }
}
