using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Schd.Notification.Api.EventBus;
using Schd.Notification.Api.EventBus.Providers;


namespace Schd.Notification.EventBus
{
    public interface IEventBus<T> where T: class
    {
        IProvider Provider { get; set; }

        public IConsumer<T> Consumer { get; set; }

        public IPublisher<T> Publisher { get; set; }

        
    }
}
