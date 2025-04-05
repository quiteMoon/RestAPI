using Microsoft.AspNetCore.Http;

namespace WebApi.BLL.Dtos.Category
{
    public class CreateCategoryDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
