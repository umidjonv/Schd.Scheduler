using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Schd.Notification.Data;
using Schd.Notification.Data.Domain;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Api.Services
{
    public class CommandService
    {
        public CommandService()
        {

        }

        public NotifyEvent SendCommand(Command command)
        {
            return new NotifyEvent
            {
                Notify = command,
                NotifyId = command.Id,
                Type = NotificationType.Command,
                State = StateType.New
            };
        }
    }
}
