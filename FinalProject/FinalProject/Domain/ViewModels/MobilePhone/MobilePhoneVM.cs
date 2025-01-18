using FinalProject.Domain.ViewModels.MobilePhoneImage;

namespace FinalProject.Domain.ViewModels.MobilePhone
{
    public class MobilePhoneVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string MainImage { get; set; }
        public string Specifications { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MobilePhoneImageVM> AdditionalImages { get; set; }
    }
}
