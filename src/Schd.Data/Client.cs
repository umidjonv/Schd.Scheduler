using System;
using System.Collections.Generic;
using System.Text;

namespace Schd.Notification.Data
{
    public class Client:BaseEntity
    {
        public string Secret { get; set; }

        public string Name { get; set; }
    }
}
