using FinalProject.Domain.ViewModels.User;
using FinalProject.Repositories;

namespace FinalProject.Utilities
{
    public class VerificationUtil
    {
        public static async Task VerifyUser(RegisterUserVM registerUserDTO, UserRepository userRepository) {

            if (registerUserDTO.Password != registerUserDTO.ConfirmPassword)
                throw new ArgumentException("Password and Confirm Password do not match.");

            if (!PasswordUtil.IsPasswordSecure(registerUserDTO.Password))
                throw new ArgumentException("Password must contain at least one capital letter and one number.");


            if (await userRepository.UserWithUsernameExists(registerUserDTO.Username))
                throw new Exception("Username is already taken.");

            if (await userRepository.UserWithEmailExists(registerUserDTO.Email))
                throw new Exception("Email is already in use.");
        }
    }
}
