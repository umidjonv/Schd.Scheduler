using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class Owners
    {
        [Key]
        public long Id { get; set; }
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