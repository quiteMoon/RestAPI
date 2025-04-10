using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Services.Account;
using WebApi.BLL.Services.User;
using WebApi.BLL.Validators.Account;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly RegisterValidator _registerValidator;
        private readonly LoginValidator _loginValidator;

        public AccountController(IAccountService accountService, IUserService userService, RegisterValidator registerValidator, LoginValidator loginValidator)
        {
            _accountService = accountService;
            _userService = userService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var validResult = await _registerValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var user = await _userService.CreateAsync(dto);

            if (user == null)
                return BadRequest("Register error");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var validResult = await _loginValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var user = await _accountService.LoginAsync(dto);

            if (user == null)
                return BadRequest("User not found");

            return Ok(user);
        }

        [HttpGet("confirmEmail")]
        public IActionResult ConfirmEmail(string? userId, string? token)
        {
            return Redirect("");
        }
    }
}
