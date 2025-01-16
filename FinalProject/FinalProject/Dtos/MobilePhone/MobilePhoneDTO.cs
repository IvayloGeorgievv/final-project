namespace FinalProject.Dtos.MobilePhone
{
    public class MobilePhoneDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model {  get; set; }
        public decimal Price {  get; set; }
        public decimal? DiscountPrice {  get; set; }
        public string MainImage {  get; set; }
        public string Specifications {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
