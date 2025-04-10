using Microsoft.AspNetCore.Http;

namespace WebApi.BLL.Dtos.AppUser
{
    public class UpdateUserDto
    {
        public required string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
