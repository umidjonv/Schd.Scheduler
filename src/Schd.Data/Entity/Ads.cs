using Schd.Data.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class Ads : AuditEntity
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public long OwnerId { get; set; }
        public virtual Owners Owners { get; set; }

        public ICollection<AdImages> AdImages { get; set; }
        public ICollection<ScheduledAds> ScheduledAds { get; set; }
    }
}