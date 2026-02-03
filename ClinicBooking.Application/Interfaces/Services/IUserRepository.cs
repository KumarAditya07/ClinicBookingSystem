using ClinicBooking.Domain.Entities;

namespace ClinicBooking.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<Role> GetRoleByNameAsync(string roleName);

        Task<User?> GetUserByEmailAsync(string email);
    }
}
