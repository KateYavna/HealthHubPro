using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Patient: BaseEntity
    {
        public Guid PersonId { get; set; }
        public ICollection<Allergy> Allergies { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public Person Person { get; set; }
    }
}