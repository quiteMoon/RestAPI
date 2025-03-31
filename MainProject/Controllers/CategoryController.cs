using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Category;
using WebApi.BLL.Services.Category;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/caterogy")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _categoryService.GetByIdAsync(id);
            return result != null ? Ok(result) : BadRequest("Category not found");
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return result ? Ok("Categoty created") : BadRequest("Category not created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryDto dto)
        {
            var result = await _categoryService.UpdateAsync(dto);
            return result ? Ok("Category updated") : BadRequest("Category not updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _categoryService.DeleteAsync(id);
            return result ? Ok("Category delete") : BadRequest("Category not created");
        }
    }
}
