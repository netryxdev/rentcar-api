using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using rentcar_api.Models;

namespace rentcar_api.Services.JWT
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey; // Chave secreta para assinatura do token
        private readonly int _expirationMinutes; // Tempo de expiração do token em minutos

        public JwtService(string secretKey, int expirationMinutes)
        {
            _secretKey = secretKey;
            _expirationMinutes = expirationMinutes;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id_user.ToString()),
                new Claim(ClaimTypes.Name, user.nm_user),
                new Claim(ClaimTypes.Email, user.nm_email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidateToken(string token)
        {
            var principal = GetPrincipalFromToken(token);
            return principal != null && principal.Identity.IsAuthenticated;
        }
    }
}
