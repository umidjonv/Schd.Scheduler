using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schd.Notification.Api.Models.Notification
{
    public class CommandMessage:IMessage
    {
        public string Command { get; set; }
    }
}
