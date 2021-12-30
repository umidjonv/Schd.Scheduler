using Schd.Data.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("TARIFF")]
    public class Tariff : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public long CurrencyId { get; set; }
    }
}