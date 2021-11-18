using Schd.Data.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class OwnerTariff : AuditEntity
    {
        [Required]
        public long OwnerId { get; set; }
        public virtual Owners Owners { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}