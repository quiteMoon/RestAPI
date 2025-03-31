using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Services.Account;
using WebApi.BLL.Services.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var user = await _userService.CreateAsync(dto);

            if (user == null)
                return BadRequest("Register error");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user = await _accountService.LoginAsync(dto);

            if (user == null)
                return BadRequest("User not found");

            return Ok(user);
        }
    }
}
