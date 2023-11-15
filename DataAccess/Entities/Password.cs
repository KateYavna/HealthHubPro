namespace DataAccess.Entities
{
    public class Password: BaseEntity
    {
        public Guid PersonId { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}