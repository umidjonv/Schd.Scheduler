using System;
using Schd.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Schd.Data.Entity.Base;

namespace Schd.Data.Entity
{
    public class Schedule : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Regularity Regularity { get; set; }
        //public Regularity Regularity { get; set; }
        public DateTime ShowTime { get; set; }
        public ICollection<ScheduledAds> ScheduledAds { get; set; }
    }
}