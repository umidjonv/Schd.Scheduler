using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Schd.Data.Entity.Base;
using Schd.Data.Enums;

namespace Schd.Scheduler.Data.Entity.Schedules
{
    [Table("SCHEDULE_TEMPLATES")]
    public class ScheduleTemplate : AuditEntity
    {
        public string Name { get; set; }
        public RegularityHours RegularityHours { get; set; }
        public RegularityDays RegularityDays { get; set; }
        public string[] CustomDates { get; set; }
        public string[] CustomTimes { get; set; }
        public bool Enabled { get; set; }
        //private ICollection<ScheduledAds> ScheduledAds { get; set; }
    }
}
