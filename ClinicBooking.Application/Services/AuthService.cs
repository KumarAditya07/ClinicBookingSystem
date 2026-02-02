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

namespace ClinicBooking.Application.Services
{
    public class AuthService : IAuthService {
        private readonly IUserRepository _userRepository;

        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task RegisterAsync(Auth.RegisterRequestDto request)
        {
            // email uniqueness check
              if(await _userRepository.EmailExistsAsync(request.Email))
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
    }
}
