using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.MobilePhone
{
    public class UpdateMobilePhoneDTO
    {
        [MaxLength(100)]
        public string Brand {  get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public decimal? Price {  get; set; }
        public decimal? DiscountPrice {  get; set; }

        public string MainImage {  get; set; }

        public string Specifications {  get; set; }
    }
}
