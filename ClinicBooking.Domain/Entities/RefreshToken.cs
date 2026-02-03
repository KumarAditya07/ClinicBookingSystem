using ClinicBooking.Domain.Common;

namespace ClinicBooking.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }


}
