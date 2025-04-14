using System.Security.Claims;

namespace WebApi.BLL.Services.JwtTokenService
{
    public interface IJwtTokenService
    {
        string CreateJwtToken(List<Claim> claims);
    }
}
