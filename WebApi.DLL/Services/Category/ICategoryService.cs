using WebApi.BLL.Dtos.Category;

namespace WebApi.BLL.Services.Category
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(UpdateCategoryDto dto);
        Task<bool> DeleteAsync(string id);
        Task<CategoryDto?> GetByIdAsync(string id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
    }
}
