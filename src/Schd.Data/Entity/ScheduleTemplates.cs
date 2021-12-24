using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Schd.Data.Entity.Base;
using Schd.Data.Enums;

namespace Schd.Data.Entity
{
    [Table("SCHEDULE_TEMPLATES")]
    public class ScheduleTemplates : AuditEntity
    {
        public string Name { get; set; }
        public RegularityHours RegularityHours { get; set; }
        public RegularityDays RegularityDays { get; set; }
        public string[] CustomDates { get; set; }
        public string[] CustomTimes { get; set; }
        public bool Enabled { get; set; }
        public ICollection<ScheduledAds> ScheduledAds { get; set; }
    }
}
