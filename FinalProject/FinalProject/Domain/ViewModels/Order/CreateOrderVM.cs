using FinalProject.Domain.ViewModels.OrderProduct;

namespace FinalProject.Domain.ViewModels.Order
{
    public class CreateOrderVM
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public List<CreateOrderProductVM> Products { get; set; }
    }
}
