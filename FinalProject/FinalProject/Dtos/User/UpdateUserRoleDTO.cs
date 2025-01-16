using FinalProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.User
{
    public class UpdateUserRoleDTO
    {
        [Required]
        [MaxLength(50)]
        public UserRoleEnum NewRole {  get; set; }
    }
}
