using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitProject.Models
{
    public class BaseModel :IBaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
