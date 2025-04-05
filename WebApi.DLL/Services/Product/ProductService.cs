using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.BLL.Dtos.Product;
using WebApi.BLL.Services.Image;
using WebApi.DAL.Entities;
using WebApi.DAL.Repositories.Category;
using WebApi.DAL.Repositories.Product;

namespace WebApi.BLL.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;

        public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryRepository categoryRepository, IImageService imageService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageService = imageService;
        }

        public async Task<ServiceResponse> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<ProductEntity>(dto);

            if (dto.Images.Count > 0)
            {
                string dirPath = Path.Combine(Settings.ProductsDir, entity.Id);

                var imagesName = await _imageService.SaveProductImages(dto.Images, dirPath);

                var imageEntities = imagesName.Select(i =>
                    new ProductImageEntity
                    {
                        Name = i,
                        Path = dirPath,
                        ProductId = entity.Id
                    });

                entity.Images = imageEntities.ToArray();
            }

            var categories = _categoryRepository
                .GetAll()
                .Where(c => dto.Categories.Select(x => x.ToUpper()).Contains(c.NormalizedName))
                .ToList();

            entity.Categories = categories;

            bool result = await _productRepository.CreateAsync(entity);

            if(result)
                return ServiceResponse.Success($"Товар '{dto.Name}' створено успішно");

            return ServiceResponse.Error("Помилка під час створення");
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var entity = await _productRepository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResponse.Error("Не вдалося видалити продукт");

            if (entity.Images.Count > 0)
            {
                _imageService.DeleteImage(Path.Combine(Settings.ProductsDir, entity.Id));
            }

            bool result = await _productRepository.DeleteAsync(entity);

            if (result)
                return ServiceResponse.Success("Продукт успішно видалено");

            return ServiceResponse.Error("Не вдалося видалити продукт");
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _productRepository
                .GetAll()
                .Include(p => p.Categories)
                .Include(p => p.Images)
                .AsNoTracking()
                .ToListAsync();

            var dtos = _mapper.Map<List<ProductDto>>(entities);

            return ServiceResponse.Success("Товари отримано", dtos);
        }

        public ServiceResponse GetByCategory(string name)
        {
            var entities = _productRepository.GetByCategory(name);

            if (entities == null)
                return ServiceResponse.Error($"Продукти з категорією {name} не знайдено");

            var dtos = _mapper.Map<List<ProductDto>>(entities);

            return ServiceResponse.Success("Продукти успішно отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var entity = await _productRepository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResponse.Error("Продукт не знайдено");

            var dto = _mapper.Map<ProductDto>(entity);

            return ServiceResponse.Success("Продукт отримано", dto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProductDto dto)
        {
            var entity = await _productRepository.GetByIdAsync(dto.Id);

            if (entity == null)
                return ServiceResponse.Error("Продукт не знайдено");

            entity = _mapper.Map(dto, entity);

            bool result = await _productRepository.UpdateAsync(entity);

            if (result)
                return ServiceResponse.Success("Продукт успішно оновлено");

            return ServiceResponse.Error("Не вдалося видалити продукт");
        }
    }
}
