using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class Tariff
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public double Price { get; set; }
        //public long CurrencyId { get; set; } // Думаю лучше сделать только на одну валюту в долларах
    }
}