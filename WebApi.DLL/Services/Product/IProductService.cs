using WebApi.BLL.Dtos.Product;

namespace WebApi.BLL.Services.Product
{
    public interface IProductService
    {
        Task<ServiceResponse> CreateAsync(CreateProductDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateProductDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetByIdAsync(string id);
        ServiceResponse GetByCategory(string name);
        Task<ServiceResponse> GetAllAsync();
    }
}
