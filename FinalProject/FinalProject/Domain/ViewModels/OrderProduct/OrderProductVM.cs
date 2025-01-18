namespace FinalProject.Domain.ViewModels.OrderProduct
{
    public class OrderProductVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MobilePhoneId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
