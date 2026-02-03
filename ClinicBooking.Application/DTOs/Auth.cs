using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Application.DTOs
{
    public  record Auth
    {
        public class RegisterRequestDto
        {
            public string FullName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
        }

        public class LoginRequestDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public class LoginResponseDto
        {
            public string AccessToken { get; set; } = null!;
            public string RefreshToken { get; set; } = null!;
        }

        public class RefreshTokenRequestDto
        {
            public string RefreshToken { get; set; } = null!;
        }

        public class RefreshTokenResponseDto
        {
            public string AccessToken { get; set; } = null!;
            public string RefreshToken { get; set; } = null!;
        }




    }
}
