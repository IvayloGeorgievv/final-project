using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.User
{
    public class LoginUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string UsernameOrEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
