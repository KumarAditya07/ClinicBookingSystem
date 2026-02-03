using ClinicBooking.Application.Common.Exceptions;
using ClinicBooking.Application.DTOs;
using ClinicBooking.Application.Interfaces.Services;
using ClinicBooking.Domain.Entities;
using ClinicBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ClinicBooking.Application.DTOs.Auth;

namespace ClinicBooking.Infrastructure.Services
{
    public partial class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
       

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task AddAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == token &&
                !x.IsRevoked &&
                x.ExpiresAt > DateTime.UtcNow);
        }

        
    }
}
