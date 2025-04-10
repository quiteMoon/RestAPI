using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Dtos.AppUser;
using WebApi.BLL.Services.Account;
using WebApi.BLL.Services.Image;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IImageService _imageService;

        public UserService(UserManager<AppUser> userManager, IAccountService accountService, IImageService imageService)
        {
            _userManager = userManager;
            _accountService = accountService;
            _imageService = imageService;
        }

        public async Task<ServiceResponse> CreateAsync(RegisterDto dto)
        {
            if (!await IsUniqueEmailAsync(dto.Email))
            {
                return ServiceResponse.Error("Користувач з цією поштою вже існує");
            }
            if (!await IsUniqueUserNameAsync(dto.UserName))
            {
                return ServiceResponse.Error("Користувач з цим іменем вже існує");
            }

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            if(dto.Image != null)
            {
                string? imageName = await _imageService.SaveImageAsync(dto.Image, Settings.UsersDir);

                if (!string.IsNullOrEmpty(imageName))
                    user.Image = Settings.UsersDir + "/" + imageName;
            }

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                byte[] bytes = Encoding.UTF8.GetBytes(token);
                token = Convert.ToBase64String(bytes);


                //await _accountService.SendEmailAsync(user.Email, "Підтвердження пошти", "");

                return ServiceResponse.Success($"Користувача '{user.UserName}' успішно створнео");
            }


            return ServiceResponse.Error("Помилка при створенні");
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return ServiceResponse.Error("Користувача не знайдено");

            if (user.Image != null)
                _imageService.DeleteImage(user.Image);

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return ServiceResponse.Success($"Користувача '{user.UserName}' успішно видалено");

            return ServiceResponse.Error("Помилка при видаленні");
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var dtos = users.Select(e => new UserDto
            {
                UserName = e.UserName ?? "Noname",
                Email = e.Email ?? ""
            });

            return ServiceResponse.Success("Користувачів успішно отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return ServiceResponse.Error("Користувача не знайдено");

            var dto = new UserDto
            {
                UserName = user.UserName ?? "Noname",
                Email = user.Email ?? ""
            };

            return ServiceResponse.Success("Користувача успішно отримано", dto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);

            if (user == null)
                return ServiceResponse.Error("Користувача не знайдено");

            if(dto.Image != null)
            {
                if (user.Image != null)
                    _imageService.DeleteImage(user.Image);

                string? imageName = await _imageService.SaveImageAsync(dto.Image, Settings.UsersDir);

                if (!string.IsNullOrEmpty(imageName))
                    user.Image = Settings.UsersDir + "/" + imageName;
            }

            user.UserName = dto.UserName;
            user.Email = dto.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return ServiceResponse.Success($"Користувача '{user.UserName}' успішно оновлено");

            return ServiceResponse.Error("Помилка при оновленні");
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
