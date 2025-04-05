using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Services.User;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
        }

        public async Task<AppUser?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return null;

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            return result ? user : null;
        }
    }
}
