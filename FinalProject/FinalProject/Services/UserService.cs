using FinalProject.Dtos.User;
using FinalProject.Enums;
using FinalProject.Models;
using FinalProject.Repositories;
using FinalProject.Utilities;
using Microsoft.Extensions.Configuration;

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

        public async Task<(string Token, UserResponseDTO User)> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (registerUserDTO.Password != registerUserDTO.ConfirmPassword)
                throw new ArgumentException("Password and Confirm Password do not match.");

            if (!PasswordHelper.IsPasswordSecure(registerUserDTO.Password))
                throw new ArgumentException("Password must contain at least one capital letter and one number.");


            if (await _userRepository.UserWithUsernameExists(registerUserDTO.Username))
                throw new Exception("Username is already taken.");

            if (await _userRepository.UserWithEmailExists(registerUserDTO.Email))
                throw new Exception("Email is already in use.");

            var user = new User
            {
                Username = registerUserDTO.Username,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Email = registerUserDTO.Email,
                Password = PasswordHelper.HashPassword(registerUserDTO.Password),
                Role = Enums.UserRoleEnum.User
            };

            user = await _userRepository.AddUser(user);

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            var userResponseDto = new UserResponseDTO
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

        public async Task<(string Token, UserResponseDTO User)> LoginUser(LoginUserDTO loginUserDTO)
        {
            var user = await _userRepository.GetUserByUsernameOrEmail(loginUserDTO.UsernameOrEmail);

            if(user == null || !PasswordHelper.VerifyPassword(loginUserDTO.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            var userResponseDto = new UserResponseDTO
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

        public async Task<IEnumerable<UserResponseDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            return users.Select(user => new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            });
        }

        public async Task<UserResponseDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if(user == null)
            {
                return null;
            }

            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            };
        }

        public async Task UpdateUser(int id, UpdateUserDTO updateUserDto)
        {
            var user = await _userRepository.GetUserById(id);

            if(user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            user.Username = updateUserDto.Username ?? user.Username;
            user.Email = updateUserDto.Email ?? user.Email;
            user.FirstName = updateUserDto.FirstName ?? user.FirstName;
            user.LastName = updateUserDto.LastName ?? user.LastName;

            await _userRepository.UpdateUser(user);
        }

        public async Task<bool> UpdateUserRole(int userId, UserRoleEnum newRole)
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
