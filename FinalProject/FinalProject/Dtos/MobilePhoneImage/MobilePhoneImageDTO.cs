using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.MobilePhoneImage
{
    public class MobilePhoneImageDTO
    {
        public int Id { get; set; }
        public int MobilePhoneId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
