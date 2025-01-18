using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.MobilePhoneImage
{
    public class CreateMobilePhoneImageVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
