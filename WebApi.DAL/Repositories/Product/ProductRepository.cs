using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Entities;

namespace WebApi.DAL.Repositories.Product
{
    public class ProductRepository 
        : GenericRepository<ProductEntity, string>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<ProductEntity?> GetByCategory(string name)
        {
            var entities =  GetAll()
                .Where(p => p.Categories.Select(c => c.NormalizedName).Contains(name.ToUpper()))
                .Include(p => p.Categories)
                .Include(p => p.Images);
            return entities;
        }

        public override Task<ProductEntity?> GetByIdAsync(string id)
        {
            var entity = _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            return entity;
        }
    }
}
