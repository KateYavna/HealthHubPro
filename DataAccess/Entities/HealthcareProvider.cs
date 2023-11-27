namespace DataAccess.Entities
{
    public class HealthcareProvider : BaseEntity
    {
        public Guid PersonId { get; set; }
        public ICollection<Specialty> Specialties { get; set; }
        public Person Person { get; set; }
    }
}