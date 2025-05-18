using System.Security.Claims;

namespace Hospital.API.Authentication.TokenFactory
{
    public interface ITokenFactory
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}