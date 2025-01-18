using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.User
{
    public class LoginUserVM
    {
        [Required]
        [MaxLength(100)]
        public string UsernameOrEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
