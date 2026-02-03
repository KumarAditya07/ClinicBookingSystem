using ClinicBooking.Application.Interfaces.Services;
using ClinicBooking.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ClinicBooking.Infrastructure.Services
{
   
        public class JwtTokenService : IJwtTokenService
        {
            private readonly IConfiguration _config;

            public JwtTokenService(IConfiguration config)
            {
                _config = config;
            }

            public string GenerateAccessToken(User user)
            {

            var keyString = _config["JwtSettings:SecretKey"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var expiryString = _config["JwtSettings:AccessTokenExpiryMinutes"];

            if (string.IsNullOrWhiteSpace(keyString))
                throw new Exception("Missing config: JwtSettings:Key");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new Exception("Missing config: JwtSettings:Issuer");

            if (string.IsNullOrWhiteSpace(audience))
                throw new Exception("Missing config: JwtSettings:Audience");

            if (!int.TryParse(expiryString, out var expiryMinutes))
                throw new Exception("Invalid config: JwtSettings:AccessTokenExpiryMinutes must be integer");




            var claims = new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.RoleName)
        };

            var key = new SymmetricSecurityKey(
 Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!)
);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["JwtSettings:AccessTokenExpiryMinutes"]!)
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
            }

            public string GenerateRefreshToken()
            {
                return Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64)
                );
            }
        }

    }

