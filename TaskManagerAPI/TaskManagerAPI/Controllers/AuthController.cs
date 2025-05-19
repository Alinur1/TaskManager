using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.BLL.Interfaces;
using TaskManagerAPI.DAL.DTOs;
using TaskManagerAPI.DAL.Models;
using TaskManagerAPI.Helpers;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _userService;

        public AuthController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
            };

            try
            {
                var result = await _userService.AddUserAsync(user);
                return Ok(new
                {
                    message = "Registration Successful",
                    user = new { result.Id, result.Username, result.Email}
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto, [FromServices] JwtHelper jwtHelper)
        {
            var user = await _userService.ValidateUserAsync(dto.Email, dto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = jwtHelper.GenerateToken(user);
            return Ok(new
            {
                message = "Login successful",
                token,
                user = new { user.Id, user.Username, user.Email }
            });
        }
    }
}
