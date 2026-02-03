using ClinicBooking.API.Common.Response;
using ClinicBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static ClinicBooking.Application.DTOs.Auth;

namespace ClinicBooking.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterRequestDto request)
        {
            await  _authService.RegisterAsync(request);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "User registered successfully",
                Data = null
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);

            return Ok(new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
    RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request);

            return Ok(new ApiResponse<RefreshTokenResponseDto>
            {
                Success = true,
                Message = "Token refreshed successfully",
                Data = result
            });
        }

    }
}
