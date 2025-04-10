using WebApi.BLL.Dtos.Role;

namespace WebApi.BLL.Services.Role
{
    public interface IRoleService
    {
        Task<ServiceResponse> CreateAsync(CreateRoleDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateRoleDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetAllAsync();
    }
}
