using ClinicBooking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Domain.Entities
{

    public class User : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public Guid RoleId { get; set; }   // FK
        public Role Role { get; set; } = null!;

       public ICollection<RefreshToken> RefreshTokens { get; set; }
    = new List<RefreshToken>();


    }


}
