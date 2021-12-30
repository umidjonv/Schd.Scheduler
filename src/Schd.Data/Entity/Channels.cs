using Schd.Data.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schd.Data.Entity
{
    [Table("CHANNELS")]
    public class Channels : AuditEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public long OwnerId { get; set; }
        public virtual Owners Owners { get; set; }
        public string Title { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}