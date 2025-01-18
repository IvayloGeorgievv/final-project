using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int MobilePhoneId { get; set; }

        [ForeignKey("MobilePhoneId")]
        public MobilePhone MobilePhone { get; set; }

        public int Quantity { get; set; } = 1;

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
