using ClinicBooking.Domain.Common;

namespace ClinicBooking.Domain.Entities
{
    public class Clinic :BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    }

}
