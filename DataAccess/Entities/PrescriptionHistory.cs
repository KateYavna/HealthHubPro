namespace DataAccess.Entities
{
    public class PrescriptionHistory: BaseEntity
    {
        public DateTime DispenseDate { get; set; }
        public int QuantityDispensed { get; set; }
        public Guid PrescriptionId { get; set; }
    }
}