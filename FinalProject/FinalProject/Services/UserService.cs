using FinalProject.Domain.Enums;
using FinalProject.Domain.Models;
using FinalProject.Domain.ViewModels.User;
using FinalProject.Repositories;
using FinalProject.Utilities;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<(string Token, UserResponseVM User)> RegisterUser(RegisterUserVM registerUserVM)
        {
            await VerificationUtil.VerifyUser(registerUserVM, _userRepository);

            var user = new User
            {
                Username = registerUserVM.Username,
                FirstName = registerUserVM.FirstName,
                LastName = registerUserVM.LastName,
                Email = registerUserVM.Email,
                Password = PasswordUtil.HashPassword(registerUserVM.Password),
                Role = UserRole.USER
            };

            user = await _userRepository.AddUser(user);

            var token = JwtGenerator.GenerateJwtToken(user, _configuration);

            var userResponseDto = new UserResponseVM
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString()
            };

            return (token, userResponseDto);
        }

        public async Task<(string Token, UserResponseVM User)> LoginUser(LoginUserVM loginUserVM)
        {
            var user = await _userRepository.GetUserByUsernameOrEmail(loginUserVM.UsernameOrEmail);

            if(user == null || !PasswordUtil.VerifyPassword(loginUserVM.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = JwtGenerator.GenerateJwtToken(user, _configuration);

            var userResponseDto = new UserResponseVM
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            };

            return (token, userResponseDto);
        }

        public async Task<IEnumerable<UserResponseVM>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            return users.Select(user => new UserResponseVM
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            });
        }

        public async Task<UserResponseVM> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if(user == null)
            {
                return null;
            }

            return new UserResponseVM
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            };
        }

        public async Task UpdateUser(int id, UpdateUserVM updateUserVM)
        {
            var user = await _userRepository.GetUserById(id);

            if(user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            user.Username = updateUserVM.Username ?? user.Username;
            user.Email = updateUserVM.Email ?? user.Email;
            user.FirstName = updateUserVM.FirstName ?? user.FirstName;
            user.LastName = updateUserVM.LastName ?? user.LastName;

            await _userRepository.UpdateUser(user);
        }

        public async Task<bool> UpdateUserRole(int userId, UserRole newRole)
        {
            var user = await _userRepository.GetUserById(userId);

            if(user == null)
            {
                return false;
            }

            user.Role = newRole;
            var updated = await _userRepository.UpdateUser(user);

            return updated != null;
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
