namespace DataAccess.Entities
{
    public class Prescription: BaseEntity
    {
        public DateTime IssueDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid HealthcareProviderId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public ICollection<PrescriptionHistory> PrescriptionHistory { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }
    }
}