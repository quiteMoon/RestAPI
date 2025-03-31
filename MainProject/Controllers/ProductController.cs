using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Product;
using WebApi.BLL.Services.Product;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _productService.GetByIdAsync(id);
            return result != null ? Ok(result) : BadRequest("Product not found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductDto dto)
        {
            var result = await _productService.CreateAsync(dto);
            return result ? Ok("Product created") : BadRequest("Product not created"); 
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductDto dto)
        {
            var result = await _productService.UpdateAsync(dto);
            return result ? Ok("Product updated") : BadRequest("Product not updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _productService.DeleteAsync(id);
            return result ? Ok("Product deleted") : BadRequest("Product not deleted");
        }
    }
}
