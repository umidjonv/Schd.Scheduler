using System;

namespace Schd.Notification.Api.EventBus.Models
{
    public class Event:IEvent
    {
        public Guid Id { get;  set; }

    }
}
