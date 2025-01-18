using FinalProject.Domain.ViewModels.MobilePhoneImage;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.MobilePhone
{
    public class UpdateMobilePhoneVM
    {
        [MaxLength(100)]
        public string Brand { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public string MainImage { get; set; }

        public string Specifications { get; set; }

        public List<CreateMobilePhoneImageVM> NewImages { get; set; }
    }
}
