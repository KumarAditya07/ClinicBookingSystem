using ClinicBooking.Application.Interfaces.Services;
using ClinicBooking.Domain.Entities;
using System.Security.Cryptography;

namespace ClinicBooking.Infrastructure.Services
{

    public class RefreshTokenService : IRefreshTokenService
    {
        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };
        }
    }


}
