using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.User
{
    public class UpdateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Username {  get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)] 
        public string LastName { get;set; }
    }
}
