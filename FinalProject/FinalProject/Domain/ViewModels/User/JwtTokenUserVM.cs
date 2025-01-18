using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.User
{
    public class JwtTokenUserVM
    {
        [Required]

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
