using Friend.Application.Contracts.Security;
using Friend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Infrastructure.Security
{
    public class TokenService(IConfiguration _config) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            var tokenKey = _config["TokenKey"];
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new InvalidOperationException("TokenKey is missing in configuration.");

            if (tokenKey.Length < 64)
                throw new InvalidOperationException("TokenKey must be at least 64 characters long for security reasons.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.DisplayName)
            };



            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
