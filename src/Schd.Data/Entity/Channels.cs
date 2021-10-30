using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class Channels
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public virtual Owners Owners { get; set; }
        public string Title { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}