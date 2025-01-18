using FinalProject.Domain.ViewModels.MobilePhoneImage;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.MobilePhone
{
    public class CreateMobilePhoneVM
    {
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required]
        public IFormFile MainImage { get; set; }

        [Required]
        public string Specifications { get; set; }

        public List<CreateMobilePhoneImageVM> AdditionalImages { get; set; }
    }
}
