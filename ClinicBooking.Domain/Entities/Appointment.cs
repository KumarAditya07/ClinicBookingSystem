using ClinicBooking.Domain.Common;
using ClinicBooking.Domain.Enums;

namespace ClinicBooking.Domain.Entities
{
    public class Appointment :BaseEntity
    {
        public DateTime AppointmentDate { get; set; }

        public DateTime ? ApprovedAt { get; set; }

        public DateTime ? RejectedAt { get; set; }    

        public Guid UserId { get; set; }   // FK

        public Guid DoctorId { get; set; } // FK

        public Guid SlotId { get; set; } // FK

        public DoctorSlot Slot { get; set; } = null!;   

        public User User { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!; 

        public AppointmentStatus Status { get; set; } 


    }

}
