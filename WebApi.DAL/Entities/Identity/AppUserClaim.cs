using Microsoft.AspNetCore.Identity;

namespace WebApi.DAL.Entities.Identity
{
    public class AppUserClaim : IdentityUserClaim<string>
    {
        public virtual AppUser? User { get; set; }
    }
}
