using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.MobilePhone
{
    public class CreateMobilePhoneDTO
    {
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model {  get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required]
        public string MainImage {  get; set; }

        [Required]
        public string Specifications { get; set; }
    }
}
