using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FinalProject.Domain.ViewModels.OrderProduct;

namespace FinalProject.Domain.ViewModels.Order
{
    public class OrderVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public List<OrderProductVM> Products { get; set; }
    }
}
