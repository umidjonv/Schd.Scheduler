using System;
using System.Collections.Generic;
using System.Text;

namespace Schd.Data
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
