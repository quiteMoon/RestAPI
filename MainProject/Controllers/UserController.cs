using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Dtos.AppUser;
using WebApi.BLL.Services.User;
using WebApi.BLL.Validators.Account;
using WebApi.BLL.Validators.Product;
using WebApi.BLL.Validators.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RegisterValidator _registerValidator;
        private readonly UpdateUserValidator _updateUserValidator;

        public UserController(IUserService UserService, RegisterValidator registerValidator, UpdateUserValidator updateUserValidator)
        {
            _userService = UserService;
            _registerValidator = registerValidator;
            _updateUserValidator = updateUserValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _userService.GetByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _userService.GetAllAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RegisterDto dto)
        {
            var validResult = await _registerValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _userService.CreateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserDto dto)
        {
            var validResult = await _updateUserValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _userService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _userService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
