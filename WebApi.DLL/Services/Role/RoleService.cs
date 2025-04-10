using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.BLL.Dtos.Role;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceResponse> CreateAsync(CreateRoleDto dto)
        {
            var entity = new AppRole
            {
                Name = dto.Name
            };

            var result = await _roleManager.CreateAsync(entity);

            if (result.Succeeded)
                return ServiceResponse.Success($"Роль '{dto.Name}' успішно створено");

            return ServiceResponse.Error("Помилка при створенні ролі");
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var entity = await _roleManager.FindByIdAsync(id);

            if (entity == null)
                return ServiceResponse.Error("Роль не знайдено");

            var result = await _roleManager.DeleteAsync(entity);

            if (result.Succeeded)
                return ServiceResponse.Success($"Роль '{entity.Name}' успішно видалено");

            return ServiceResponse.Error("Помилка при видаленні");
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _roleManager.Roles.ToListAsync();

            var dtos = entities.Select(e => new RoleDto
            {
                Id = e.Id,
                Name = e.Name ?? "Noname"
            });

            return ServiceResponse.Success("Ролі отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var entity = await _roleManager.FindByIdAsync(id);

            if(entity == null)
                return ServiceResponse.Error("Роль не знайдено");

            var dto = new RoleDto
            {
                Id = entity.Id,
                Name = entity.Name ?? "Noname"
            };

            return ServiceResponse.Success("Роль отримано успішно", dto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateRoleDto dto)
        {
            var entity = new AppRole
            {
                Id = dto.Id,
                Name = dto.Name
            };

            var result = await _roleManager.UpdateAsync(entity);

            if (result.Succeeded)
                return ServiceResponse.Success($"Роль '{entity.Name}' успішно оновлено");

            return ServiceResponse.Error("Помилка при оновленні");
        }
    }
}
