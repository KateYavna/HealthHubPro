using DataAccess.Entities;

namespace Services.Models
{
    public class PatientData
    {
        public Person Person { get; set; }
        public Address Address { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public Patient Patient { get; set; }
        public List<Allergy> CommonAllergies { get; set; }
        public List<HealthcareProvider> HealthcareProviders { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Prescription> Prescriptions { get; set; }

    }
}