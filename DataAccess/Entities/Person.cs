namespace DataAccess.Entities
{
    public class Person: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid AddressId { get; set; }
        public Guid EmergencyContactId { get; set; }
        public Guid PasswordId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}