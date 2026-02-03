using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClinicBooking.Application.DTOs.Auth;

namespace ClinicBooking.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto request);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

          Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
