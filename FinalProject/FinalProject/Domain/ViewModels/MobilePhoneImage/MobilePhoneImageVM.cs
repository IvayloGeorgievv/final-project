using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.MobilePhoneImage
{
    public class MobilePhoneImageVM
    {
        public int Id { get; set; }
        public int MobilePhoneId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
