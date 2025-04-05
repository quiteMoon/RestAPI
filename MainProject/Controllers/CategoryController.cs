using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Category;
using WebApi.BLL.Services.Category;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id, string? name)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var responseId = await _categoryService.GetByIdAsync(id);
                return responseId.IsSuccess ? Ok(responseId) : BadRequest(responseId);
            }

            if (!string.IsNullOrEmpty(name))
            {
                var responseName = await _categoryService.GetByNameAsync(name);
                return responseName.IsSuccess ? Ok(responseName) : BadRequest(responseName);
            }

            var response = await _categoryService.GetAllAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
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
            var response = await _categoryService.CreateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryDto dto)
        {
            var response = await _categoryService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _categoryService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
