using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schd.Notification.Api.EventBus.Consts
{
    public static class Constants
    {
        public const string DEFAULT_EXCHANGE = "default_exchange";
        public const string COMMAND_QUEUE = "command_queue";
        public const string LOG_QUEUE = "log_queue";
        public const string MESSAGE_QUEUE = "message_queue";
    }
}
