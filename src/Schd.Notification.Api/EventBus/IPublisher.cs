using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schd.Notification.EventBus
{
    public interface IPublisher<in T> where T:class
    {
        void Publish(T model);

    }
}
