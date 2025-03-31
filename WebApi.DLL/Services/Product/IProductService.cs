using WebApi.BLL.Dtos.Product;

namespace WebApi.BLL.Services.Product
{
    public interface IProductService
    {
        Task<bool> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(UpdateProductDto dto);
        Task<bool> DeleteAsync(string id);
        Task<ProductDto?> GetByIdAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
    }
}
