using Microsoft.AspNetCore.Http;

namespace WebApi.BLL.Dtos.Product
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }        
        public decimal Price { get; set; }        
        public int Amount { get; set; }
        
        public List<string> Categories { get; set; } = [];
        public List<IFormFile> Images { get; set; } = [];
    }
}
