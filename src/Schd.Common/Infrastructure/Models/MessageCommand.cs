using System;
using System.Collections.Generic;
using System.Text;

namespace Schd.Common.Infrastructure.Models
{
    public class MessageCommand: IMessage<Command>
    {
        public Command Message { get; set; }
    }
}
