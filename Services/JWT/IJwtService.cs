using rentcar_api.Models;
using System.Security.Claims;

namespace rentcar_api.Services.JWT
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal GetPrincipalFromToken(string token);
        bool ValidateToken(string token);
    }
}
