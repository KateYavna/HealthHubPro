namespace DataAccess.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}