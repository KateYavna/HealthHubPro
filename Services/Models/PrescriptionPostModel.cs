namespace Services.Models
{
    public class PrescriptionPostModel
    {
        public Guid Id { get; set; }
        public DateTime IssueDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid HealthcareProviderId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
    }
}