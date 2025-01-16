namespace FinalProject.Dtos.OrderProduct
{
    public class OrderProductDTO
    {
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public int MobilePhoneId {  get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
    }
}
