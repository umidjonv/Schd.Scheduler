using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schd.Notification.Models.EventBus
{
    public class NotifyMessage
    {
        public Guid Id { get; set; }

        public string ServiceName { get; set; }

        public string Message { get; set; }

        public DateTime SendDate { get; set; }

    }
}
