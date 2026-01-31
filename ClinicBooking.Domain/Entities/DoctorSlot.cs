using ClinicBooking.Domain.Common;

namespace ClinicBooking.Domain.Entities
{
    public class DoctorSlot : BaseEntity
    {
        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsBooked { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }

}
