using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitProject.Rabbit
{
    public interface IRabbitConsumer<T>
    {
        void Consume();
    }
}
