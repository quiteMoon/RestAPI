using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Dtos.AppUser;
using WebApi.BLL.Services.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _userService.GetByIdAsync(id);
            return result != null ? Ok(result) : BadRequest("User not found");
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RegisterDto dto)
        {
            var result = await _userService.CreateAsync(dto);
            return result != null ? Ok(result) : BadRequest("User not created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserDto dto)
        {
            var result = await _userService.UpdateAsync(dto);
            return result ? Ok("User updated") : BadRequest("User not updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _userService.DeleteAsync(id);
            return result ? Ok("User delete") : BadRequest("User not created");
        }
    }
}
