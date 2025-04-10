using Microsoft.AspNetCore.Http;

namespace WebApi.BLL.Dtos.Account
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
