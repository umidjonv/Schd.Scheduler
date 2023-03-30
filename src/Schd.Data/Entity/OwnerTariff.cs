using Schd.Data.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("OWNER_TARIFF")]
    public class OwnerTariff : AuditEntity
    {
        [Required]
        public long OwnerId { get; set; }
        public virtual Owner Owners { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}