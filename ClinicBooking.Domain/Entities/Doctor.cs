using ClinicBooking.Domain.Common;

namespace ClinicBooking.Domain.Entities
{
    public class Doctor : BaseEntity
    {

        public string Specialty { get; set; } = null!;

        public Guid UserId { get; set; }   // FK

        public Guid ClinicId { get; set; } // FK

        public User User { get; set; } = null!;

        public Clinic Clinic { get; set; } = null!;

        public ICollection<DoctorSlot> Slots { get; set; } = new List<DoctorSlot>();



    }

}
