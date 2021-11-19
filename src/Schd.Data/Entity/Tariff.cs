using Schd.Data.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class Tariff : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        //public long CurrencyId { get; set; } // Думаю лучше сделать только на одну валюту в долларах
    }
}