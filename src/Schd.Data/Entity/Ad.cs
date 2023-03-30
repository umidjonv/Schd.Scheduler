using Schd.Data.Entity.Base;
using Schd.Scheduler.Data.Entity.Schedules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("ADS")]
    public class Ad : AuditEntity
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public long OwnerId { get; set; }
        public virtual Owner Owners { get; set; }

        public ICollection<AdImage> AdImages { get; set; }
        public ICollection<ScheduledAd> ScheduledAds { get; set; }
    }
}