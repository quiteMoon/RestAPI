using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Services.EmailService;
using WebApi.BLL.Services.Image;
using WebApi.BLL.Services.JwtTokenService;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IImageService _imageService;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountService(UserManager<AppUser> userManager, IImageService imageService, IEmailService emailService, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _imageService = imageService;
            _emailService = emailService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return ServiceResponse.Error("Користувача не знайдено");

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result)
                return ServiceResponse.Error("Пароль вказано не вірно");

            var claims = new List<Claim>
            {
                new Claim("userId", user.Id),
                new Claim("email", user.Email ?? ""),
                new Claim("userName", user.UserName ?? ""),
                new Claim("firstName", user.FirstName ?? ""),
                new Claim("lastName", user.LastName ?? "")
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            string jwtToken = _jwtTokenService.CreateJwtToken(claims);

            return ServiceResponse.Success("Успішний вхід", jwtToken);
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterDto dto)
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

            if (dto.Image != null)
            {
                string? imageName = await _imageService.SaveImageAsync(dto.Image, Settings.UsersDir);

                if (!string.IsNullOrEmpty(imageName))
                    user.Image = Settings.UsersDir + "/" + imageName;
            }

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await SendEmailConfirmMessageAsync(user);

                var claims = new List<Claim>
                {
                    new Claim("userId", user.Id),
                    new Claim("email", user.Email ?? ""),
                    new Claim("userName", user.UserName ?? ""),
                    new Claim("firstName", user.FirstName ?? ""),
                    new Claim("lastName", user.LastName ?? ""),
                    new Claim("roles", string.Join(",", user.UserRoles.Select(r => r.Role.Name)))
                };

                var jwtToken = _jwtTokenService.CreateJwtToken(claims);

                return ServiceResponse.Success("Реєстрація успішна", jwtToken);
            }

            return ServiceResponse.Error("Помилка при створенні");
        }

        private async Task SendEmailConfirmMessageAsync(AppUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] bytes = Encoding.UTF8.GetBytes(token);
            token = Convert.ToBase64String(bytes);

            string htmlPath = Path.Combine(Settings.RootPath ?? "/", "templates", "html", "confirm_email.html");
            string html = File.ReadAllText(htmlPath);
            string url = $"https://localhost:7089/api/account/confirmEmail?userId={user.Id}&token={token}";
            html = html.Replace("action_url", url);

            await _emailService.SendMessageAsync(user.Email ?? "", "Підтвердження пошти", html, true);
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return ServiceResponse.Error("Користувача не знайдено");

            byte[] bytes = Convert.FromBase64String(token);
            string confirmToken = Encoding.UTF8.GetString(bytes);

            var result =  await _userManager.ConfirmEmailAsync(user, confirmToken);

            if (result.Succeeded)
                return ServiceResponse.Success("Пошту успішно підтверджено");

            return ServiceResponse.Error("Помилка при підтвердженні пошти");
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
