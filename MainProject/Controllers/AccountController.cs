using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Services.Account;
using WebApi.BLL.Validators.Account;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly RegisterValidator _registerValidator;
        private readonly LoginValidator _loginValidator;

        public AccountController(IAccountService accountService, RegisterValidator registerValidator, LoginValidator loginValidator)
        {
            _accountService = accountService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var validResult = await _registerValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _accountService.RegisterAsync(dto);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var validResult = await _loginValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _accountService.LoginAsync(dto);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string? userId, string? token)
        {
            if (userId == null || token == null)
                return BadRequest("UserId or token is null");

            var response = await _accountService.ConfirmEmailAsync(userId, token);

            return Redirect("");
        }
    }
}
