﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Schd.Common;
using Schd.Notification.Data.Domain;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Data
{
    [Table("logs")]
    public class Log : BaseEntity, INotify
    {
        public string Message { get; set; }

        public LoggingType Type { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual List<State> States { get; set; }

        public virtual List<StateHistory> StateHistories { get; set; }

    }
}