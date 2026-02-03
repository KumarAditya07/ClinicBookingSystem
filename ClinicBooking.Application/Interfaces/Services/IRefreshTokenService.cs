using ClinicBooking.Domain.Entities;

namespace ClinicBooking.Application.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        RefreshToken GenerateRefreshToken(Guid userId);


    }

}
