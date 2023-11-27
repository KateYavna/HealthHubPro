namespace Services.Models
{
    public class AppointmentPostModel
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid PatientId { get; set; }
        public Guid HealthcareProviderId { get; set; }
        public string AppointmentType { get; set; }
    }
}