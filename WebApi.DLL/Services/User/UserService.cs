using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Dtos.AppUser;
using WebApi.BLL.Services.Account;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;

        public UserService(UserManager<AppUser> userManager, IAccountService accountService)
        {
            _userManager = userManager;
            _accountService = accountService;
        }

        public async Task<AppUser?> CreateAsync(RegisterDto dto)
        {
            if (!await IsUniqueEmailAsync(dto.Email))
            {
                return null;
            }
            if (!await IsUniqueUserNameAsync(dto.UserName))
            {
                return null;
            }

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                byte[] bytes = Encoding.UTF8.GetBytes(token);
                token = Convert.ToBase64String(bytes);


                //await _accountService.SendEmailAsync(user.Email, "Підтвердження пошти", "");

                return user;
            }
                

            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var dtos = users.Select(e => new UserDto
            {
                UserName = e.UserName ?? "Noname",
                Email = e.Email ?? ""
            });

            return dtos;
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;

            var dto = new UserDto
            {
                UserName = user.UserName ?? "Noname",
                Email = user.Email ?? ""
            };

            return dto;
        }

        public async Task<bool> UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);

            if (user == null)
                return false;

            user.UserName = dto.UserName;
            user.Email = dto.Email;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        private async Task<bool> IsUniqueEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }

        private async Task<bool> IsUniqueUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user == null;
        }
    }
}
