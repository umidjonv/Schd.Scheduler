using System;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity.Base
{
    public abstract class AuditEntity : BaseEntity
    {
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedIp { get; set; }
        [ScaffoldColumn(false)]
        public DateTime ModifiedDate { get; set; }
        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public string ModifiedIp { get; set; }
    }
}
