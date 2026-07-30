using E_Commerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Services
{
    internal class TokenServices : ITokenServices
    {
        private readonly JwtSettings _options;

        public TokenServices(IOptions<JwtSettings> options)
        {
            _options=options.Value;
        }
        public string CreateToken(string userId, string email, string userNamed, IEnumerable<string> roles)
        {
            
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name,userNamed),
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            if (string.IsNullOrEmpty(_options.SecretKey))
                throw new InvalidOperationException("Jwt Secret is missing");
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

       
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMieutes),
                signingCredentials: credentials 
            );

         
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
    public class JwtSettings
    {
        public string SecretKey { get; set; } =  default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;

        public int ExpirationMieutes { get; set; } = 60;

    }
}
