﻿using System;
using System.Collections.Generic;
using System.Text;
using Schd.Data.Entity.Base;

namespace Schd.Data.Entity
{
    public class ScheduleTemplates : AuditEntity
    {
        public string Name { get; set; }
        public RegularityHours RegularityHours { get; set; }
        public RegularityDays RegularityDays { get; set; }
        public string[] CustomDates { get; set; }
        public string[] CustomTimes { get; set; }
        public bool Enabled { get; set; }
        private ICollection<ScheduledAds> ScheduledAds { get; set; }
    }
}