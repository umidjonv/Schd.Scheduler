using System;

namespace Schd.Notification.Data
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
