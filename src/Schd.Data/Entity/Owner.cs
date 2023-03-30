using Schd.Data.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("OWNERS")]
    public class Owner : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Ad> Ads{ get; set; }
        public virtual ICollection<Channel> Channels { get; set; }
        public virtual ICollection<OwnerTariff> OwnerTariffs { get; set; }

    }
}