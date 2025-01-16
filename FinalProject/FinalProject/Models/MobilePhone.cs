using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class MobilePhone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength (100)]
        public string Model {  get; set; }

        [Required]
        public decimal Price {  get; set; }
        public decimal? DiscountPrice { get; set; }

        [Required]
        public string MainImage {  get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public string Specifications { get; set; }
        
        public DateTime CreatedAt {  get; set; } = DateTime.UtcNow;
    }
}
