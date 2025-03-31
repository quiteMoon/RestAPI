using WebApi.BLL.Dtos.Role;

namespace WebApi.BLL.Services.Role
{
    public interface IRoleService
    {
        Task<bool> CreateAsync(CreateRoleDto dto);
        Task<bool> UpdateAsync(UpdateRoleDto dto);
        Task<bool> DeleteAsync(string id);
        Task<RoleDto?> GetByIdAsync(string id);
        Task<IEnumerable<RoleDto>> GetAllAsync();
    }
}
