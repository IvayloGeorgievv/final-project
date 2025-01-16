using FinalProject.Dtos.MobilePhone;
using FinalProject.Dtos.MobilePhoneImage;
using FinalProject.Dtos.User;
using FinalProject.Repositories;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/[controller]")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly MobilePhoneService _mobilePhoneService;

        public AdminController(UserService userService, MobilePhoneService mobilePhoneService)
        {
            _userService = userService;
            _mobilePhoneService = mobilePhoneService;
        }

        /*
         *  User Methods
         */

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new {Message = "Error: " + ex.Message});
            }
        }

        [HttpPost("UpdateUserRole/{id}")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDTO updateUserRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userService.UpdateUserRole(id, updateUserRoleDTO.NewRole);
                return Ok(new { Message = "User role updated successfully." });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new {Message = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }

        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try 
            {
                var result = await _userService.DeleteUser(id);

                if (!result)
                {
                    return NotFound(new { Message = "User not found or delete failed." });
                }

                return Ok(new { Message = "User deleted successfully" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }

        }

        /*
        *  MobilePhone Methods
        */

        [HttpGet("GetAllMobilePhones")]
        public async Task<IActionResult> GetAllMobilePhones()
        {
            try
            {
                var phones = await _mobilePhoneService.GetMobilePhones();

                return Ok(phones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }
        }

        [HttpPost("CreateMobilePhone")]
        public async Task<IActionResult> CreateMobilePhone([FromBody] CreateMobilePhoneDTO createMobilePhoneDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _mobilePhoneService.AddMobilePhone(createMobilePhoneDTO);

                return Ok(new { Message = "Mobile phone created successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {Message = "Error: " + ex.Message});
            }
        }

        [HttpPut("UpdateMobilePhone/{id}")]
        public async Task<IActionResult> UpdateMobilePhone(int id,[FromBody]  UpdateMobilePhoneDTO updateMobilePhoneDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _mobilePhoneService.UpdateMobilePhone(id, updateMobilePhoneDTO);

                if (result == null)
                {
                    return NotFound(new { Message = "Mobile phone not found or update failed." });
                }

                return Ok(new { Message = "Mobile phone updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }
        }

        [HttpDelete("DeleteMobilePhone/{id}")]
        public async Task<IActionResult> DeleteMobilePhone(int id)
        {
            try
            {
                var result = await _mobilePhoneService.DeleteMobilePhone(id);

                if (!result)
                {
                    return NotFound(new { Message = "Mobile phone not found or delete failed." });
                }

                return Ok(new { Message = "Mobile phone deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }
        }

        /*
        *  MobilePhoneImages Methods
        */

       
    }
}
