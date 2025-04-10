using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Dtos.Role;
using WebApi.BLL.Services.Role;
using WebApi.BLL.Validators.Role;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly UpdateRoleValidator _updateRoleValidator;
        private readonly CreateRoleValidator _createRoleValidator;

        public RoleController(IRoleService roleService, UpdateRoleValidator updateRoleValidator, CreateRoleValidator createRoleValidator)
        {
            _roleService = roleService;
            _updateRoleValidator = updateRoleValidator;
            _createRoleValidator = createRoleValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRoleDto dto)
        {
            var validResult = await _createRoleValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _roleService.CreateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateRoleDto dto)
        {
            var validResult = await _updateRoleValidator.ValidateAsync(dto);

            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var response = await _roleService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _roleService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var response = await _roleService.GetByIdAsync(id);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _roleService.GetAllAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
