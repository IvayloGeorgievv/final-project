namespace FinalProject.Dtos.Order
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
    }
}
