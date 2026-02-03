using ClinicBooking.Application.Common.Exceptions;
using ClinicBooking.Application.DTOs;
using ClinicBooking.Application.Interfaces.Services;
using ClinicBooking.Domain.Entities;
using ClinicBooking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClinicBooking.Application.DTOs.Auth;
// Add this using directive at the top with the others

namespace ClinicBooking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;


        public AuthService(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task RegisterAsync(Auth.RegisterRequestDto request)
        {
            // email uniqueness check
            if (await _userRepository.EmailExistsAsync(request.Email))
            {
                throw new AppException("Email already in use.");

            }

            // Business rule: assign Patient role
            var patientRole = await _userRepository.GetRoleByNameAsync("Patient");

            // Create new user
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleId = patientRole.Id
            };

            // 4. Hash password (business security rule)

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);

            // 5. Save user to database
            await _userRepository.AddUserAsync(newUser);


        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            var verificationResult = _passwordHasher.VerifyHashedPassword(
                user,
                 user.PasswordHash,
                   request.Password
               );

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new AppException("Invalid email or password");
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken(user.Id);

           
            await _refreshTokenRepository.AddAsync(refreshToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<Auth.RefreshTokenResponseDto> RefreshTokenAsync(Auth.RefreshTokenRequestDto request)
        {
            var existingToken =
        await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (existingToken == null)
                throw new AppException("Invalid refresh token.");

            if (existingToken.IsRevoked)
                throw new AppException("Refresh token revoked.");

            if (existingToken.ExpiresAt < DateTime.UtcNow)
                throw new AppException("Refresh token expired.");

            // 🔒 Rotate token
            existingToken.IsRevoked = true;

            var newAccessToken =
                _jwtTokenService.GenerateAccessToken(existingToken.User);

            var newRefreshToken =
                _refreshTokenService.GenerateRefreshToken(existingToken.UserId);

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };

        }
    }
}
