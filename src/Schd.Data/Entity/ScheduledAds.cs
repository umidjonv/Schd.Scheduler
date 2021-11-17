using System;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class ScheduledAds
    {
        [Key]
        public long Id { get; set; }
        public long AdId{ get; set; }
        public virtual Ads Ads { get; set; }
        public long SchedulerId { get; set; }
        public virtual Schedule Schedules { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsComplited { get; set; }
    }
}