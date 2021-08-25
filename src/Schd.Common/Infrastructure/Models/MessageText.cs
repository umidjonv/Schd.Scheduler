using System;
using System.Collections.Generic;
using System.Text;

namespace Schd.Common.Infrastructure.Models
{
    public class MessageText: IMessage<string>
    {
        public string Message { get; set; }
    }
}
