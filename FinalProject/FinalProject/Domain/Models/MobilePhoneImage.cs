using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Domain.Models
{
    public class MobilePhoneImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MobilePhoneId { get; set; }

        [ForeignKey("MobilePhoneId")]
        public MobilePhone MobilePhone { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
