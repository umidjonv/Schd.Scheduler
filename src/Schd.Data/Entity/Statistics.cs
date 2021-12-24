using Schd.Data.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("STATISTICS")]
    public class Statistics : BaseEntity
    {
        public DateTime RevisionDate { get; set; }
        public int UsersCount { get; set; }
        public int Leaved { get; set; }
        public int Incomed { get; set; }
    }
}