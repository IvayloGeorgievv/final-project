using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.ViewModels.User
{
    public class UpdateUserRoleVM
    {
        [Required]
        [MaxLength(50)]
        public UserRole NewRole { get; set; }
    }
}
