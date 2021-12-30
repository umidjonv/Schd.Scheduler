using Schd.Data.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("OWNERS")]
    public class Owners : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Ads> Ads{ get; set; }
        public ICollection<Channels> Channels { get; set; }
        public ICollection<OwnerTariff> OwnerTariffs { get; set; }

    }
}