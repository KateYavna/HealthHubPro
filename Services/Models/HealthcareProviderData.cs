using DataAccess.Entities;

namespace Services.Models
{
     public class HealthcareProviderData
    {
        public Person Person { get; set; }  
        public Address Address { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public List<Specialty> CommonSpecialties { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}