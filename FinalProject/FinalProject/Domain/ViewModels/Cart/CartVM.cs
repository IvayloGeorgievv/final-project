using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.Cart
{
    public class CartVM
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MobilePhoneId { get; set; }

        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}
