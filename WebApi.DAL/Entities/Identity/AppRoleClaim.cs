using Microsoft.AspNetCore.Identity;

namespace WebApi.DAL.Entities.Identity
{
    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole? Role { get; set; }
    }
}
