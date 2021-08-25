using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Schd.Common.Infrastructure.Models;

namespace Schd.Common.Infrastructure.Extensions
{
    public static class MessageConvert
    {
        public static string GetMessage(this MessageText message)
        {
            return message.Message;
        }

        public static string GetMessage(this MessageCommand message)
        {
            return JsonConvert.SerializeObject(message.Message);
        }

    }
}
