using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Dtos.AppUser;

namespace WebApi.BLL.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse> CreateAsync(RegisterDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateUserDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetAllAsync();
    }
}
