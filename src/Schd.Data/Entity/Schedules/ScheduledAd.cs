using Schd.Data.Entity;
using Schd.Data.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Scheduler.Data.Entity.Schedules
{
    [Table("SCHEDULED_ADS")]
    public class ScheduledAd : AuditEntity
    {
        [Required]
        public long AdId { get; set; }
        public virtual Ad Ads { get; set; }
        [Required]
        public long SchedulerId { get; set; }
        public virtual ScheduleTemplate ScheduleTemplates { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsComplited { get; set; }
    }
}