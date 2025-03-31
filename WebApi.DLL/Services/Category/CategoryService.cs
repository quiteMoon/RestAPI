using Microsoft.EntityFrameworkCore;
using WebApi.BLL.Dtos.Category;
using WebApi.DAL;
using WebApi.DAL.Entities;

namespace WebApi.BLL.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CreateCategoryDto dto)
        {
            var entity = new CategoryEntity
            {
                Name = dto.Name
            };

            await _context.Categories.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return false;

            _context.Categories.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var entities = await _context.Categories.ToListAsync();

            var dtos = entities.Select(e => new CategoryDto
            {
                Id = e.Id,
                Name = e.Name
            });

            return dtos;
        }

        public async Task<CategoryDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return null;

            var dto = new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return dto;
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = new CategoryEntity
            {
                Id = dto.Id,
                Name = dto.Name
            };

            _context.Categories.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
