using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Schd.Notification.Api.EventBus;
using Schd.Notification.Api.EventBus.Providers;


namespace Schd.Notification.EventBus
{
    public interface IRabbitEventBus<T> where T :class
    {
        public IConsumer<T> Consumer { get; set; }

        public IPublisher<T> Publisher { get; set; }

    }
}
