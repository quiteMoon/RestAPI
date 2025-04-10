using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Product;
using WebApi.BLL.Services.Product;
using WebApi.BLL.Validators.Category;
using WebApi.BLL.Validators.Product;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly CreateProductValidator _createProductValidator;
        private readonly UpdateProductValidator _updateProductValidator;

        public ProductController(IProductService productService, CreateProductValidator createProductValidator, UpdateProductValidator updateProductValidator)
        {
            _productService = productService;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _productService.GetByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("category")]
        public IActionResult GetByCategoryAsync(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return NotFound();

            var response = _productService.GetByCategory(name);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _productService.GetAllAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductDto dto)
        {
            var validResult = await _createProductValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _productService.CreateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductDto dto)
        {
            var validResult = await _updateProductValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _productService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _productService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
