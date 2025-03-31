using Microsoft.EntityFrameworkCore;
using WebApi.BLL.Dtos.Product;
using WebApi.DAL;
using WebApi.DAL.Entities;

namespace WebApi.BLL.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CreateProductDto dto)
        {
            var entity = new ProductEntity
            {
                Name = dto.Name,
                Amount = dto.Amount,
                Description = dto.Description,
                Price = dto.Price
            };

            await _context.Products.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Products
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return false;

            _context.Products.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var entities = await _context.Products.ToListAsync();

            var dtos = entities.Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Amount = e.Amount,
                Price = e.Price
            });

            return dtos;
        }

        public async Task<ProductDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Products
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return null;

            var dto = new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Amount = entity.Amount,
                Price = entity.Price,
                Description = entity.Description
            };

            return dto;
        }

        public async Task<bool> UpdateAsync(UpdateProductDto dto)
        {
            var entity = new ProductEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Amount = dto.Amount,
                Description = dto.Description,
                Price = dto.Price
            };

            _context.Products.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
