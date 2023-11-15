using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class AddressRepository : Repository<Address>
    {
        public AddressRepository(HealthHubDbContext context) : base(context) { }
    }
}