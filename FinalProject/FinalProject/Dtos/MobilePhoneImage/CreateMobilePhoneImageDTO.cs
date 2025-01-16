using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.MobilePhoneImage
{
    public class CreateMobilePhoneImageDTO
    {
        [Required]
        public string ImageUrl {  get; set; }
    }
}
