namespace DataAccess.Entities
{
    public class EmergencyContact: BaseEntity
    {
        public string Name { get; set; }

        public string Relationship { get; set; }

        public string PhoneNumber { get; set; }
    }
}