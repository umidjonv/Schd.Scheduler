using System;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class OwnerTariff
    {
        [Key]
        public long Id{ get; set; }
        public long OwnerId { get; set; }
        public virtual Owners Owners { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}