using FinalProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.User
{
    public class JwtTokenUserDTO
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
        public UserRoleEnum Role { get; set; }
    }
}
