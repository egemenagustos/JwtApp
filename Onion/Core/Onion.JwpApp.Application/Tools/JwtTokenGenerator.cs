using Microsoft.IdentityModel.Tokens;
using Onion.JwpApp.Application.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onion.JwtApp.API.Tools
{
    public static class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(CheckUserRequestDto dto)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(dto.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));

            if (!string.IsNullOrEmpty(dto.UserName))
            {
                claims.Add(new Claim("username", dto.UserName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(

            issuer: JwtTokenDefaults.ValidIssuer,

            audience: JwtTokenDefaults.ValidAudience,

            claims: claims, notBefore: DateTime.UtcNow,

            expires: DateTime.UtcNow.AddDays(5),

            signingCredentials: signInCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
