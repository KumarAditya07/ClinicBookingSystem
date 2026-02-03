using ClinicBooking.Domain.Entities;

namespace ClinicBooking.Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
       
    }

}
