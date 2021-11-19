using Schd.Data.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class AdImages : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public long AdId{ get; set; }
        public virtual Ads Ads { get; set; }
    }
}