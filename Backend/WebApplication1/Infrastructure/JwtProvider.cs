using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        public const string SecretKey  = "SecretSecretSecrxzcxzcxzczxcxzczxcxzcxcxczxcxzczxczxczxccxzcxzczxcz";

        public const int ExpiresHours = 1;
        public string GenerateToken(UserEntity user)
        {
            Claim[] claims = {  new Claim("userId", user.Id.ToString()),
                                new Claim("company", user.Company.ToString()),
                                new Claim("status", user.Status.ToString())};

            var signingCredentials = new SigningCredentials(
                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(ExpiresHours),
                claims: claims);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
