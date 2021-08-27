using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace Schd.Notification.EventBus
{
    public interface IEventBus<T> where T: class
    {
        public IConsumer<T> Consumer { get; set; }
        
    }
}
