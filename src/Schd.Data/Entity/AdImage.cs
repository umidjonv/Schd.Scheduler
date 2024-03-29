using Schd.Data.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("ADS_IMAGES")]
    public class AdImage : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public long AdId{ get; set; }
        public virtual Ad Ads { get; set; }
    }
}