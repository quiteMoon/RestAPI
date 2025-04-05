using WebApi.DAL.Entities;

namespace WebApi.DAL.Repositories.Category
{
    public interface ICategoryRepository 
        : IGenericRepository<CategoryEntity, string>
    {
        Task<CategoryEntity?> GetByNameAsync(string name);
        bool IsUniqueName(string name);
    }
}
