﻿using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos.User
{
    public class RegisterUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string FirstName {  get; set; }

        [MaxLength (100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string ConfirmPassword { get; set; }

    }
}
