using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.User
{
    public class CreateUserVM
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
