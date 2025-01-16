using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId {  get; set; }

        [ForeignKey("OrderId")]
        public Order Order {  get; set; }

        [Required]
        public int MobilePhoneId { get; set; }

        [ForeignKey("MobilePhoneId")]
        public MobilePhone MobilePhone { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

    }
}
