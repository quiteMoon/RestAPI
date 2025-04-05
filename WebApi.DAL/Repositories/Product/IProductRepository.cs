using WebApi.DAL.Entities;

namespace WebApi.DAL.Repositories.Product
{
    public interface IProductRepository
        : IGenericRepository<ProductEntity, string>
    {
        IQueryable<ProductEntity?> GetByCategory(string name);
    }
}
