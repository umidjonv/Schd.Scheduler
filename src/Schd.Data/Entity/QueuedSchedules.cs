using Schd.Data.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Schd.Data.Enums;

namespace Schd.Data.Entity
{
    [Table("QUEUED_SCHEDULES")]
    public class QueuedSchedules : AuditEntity
    {
        public long TemplateId {get;set;}
        public ScheduleTemplates Template {get;set;}
        public DateTime StartTime {get;set;}
        public JobStatus Status {get;set;}
    }
}