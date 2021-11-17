using System;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class Statistics
    {
        [Key]
        public long Id { get; set; }
        public DateTime RevisionDate { get; set; }
        public int Leaved { get; set; }
        public int Incomed { get; set; }
    }
}