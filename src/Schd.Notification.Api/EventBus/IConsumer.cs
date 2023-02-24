using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schd.Notification.Api.EventBus
{
    public interface IConsumer<T> where T :class
    {
        public void Consume(T model);

    }
}
