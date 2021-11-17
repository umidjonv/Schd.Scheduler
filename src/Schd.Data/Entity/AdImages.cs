using System.ComponentModel.DataAnnotations;

namespace Schd.Data.Entity
{
    public class AdImages
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public long AdId{ get; set; }
        public virtual Ads Ads { get; set; }
    }
}