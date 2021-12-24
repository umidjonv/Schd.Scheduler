using Schd.Data.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("SCHEDULED_ADS")]
    public class ScheduledAds :AuditEntity
    {
        [Required]
        public long AdId{ get; set; }
        public virtual Ads Ads { get; set; }
        [Required]
        public long SchedulerId { get; set; }
        public virtual ScheduleTemplates ScheduleTemplates { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsComplited { get; set; }
    }
}