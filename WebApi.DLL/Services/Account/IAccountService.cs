using WebApi.BLL.Dtos.Account;
using WebApi.DAL.Entities.Identity;

namespace WebApi.BLL.Services.Account
{
    public interface IAccountService
    {
        Task<AppUser?> LoginAsync(LoginDto dto);
    }
}
