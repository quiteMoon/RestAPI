using WebApi.BLL.Dtos.Account;
using WebApi.BLL.Dtos.AppUser;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.User
{
    public interface IUserService
    {
        Task<AppUser?> CreateAsync(RegisterDto dto);
        Task<bool> UpdateAsync(UpdateUserDto dto);
        Task<bool> DeleteAsync(string id);
        Task<UserDto?> GetByIdAsync(string id);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
