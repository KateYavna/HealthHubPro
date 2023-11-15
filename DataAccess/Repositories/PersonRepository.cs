using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PersonRepository: Repository<Person>
    {
        public PersonRepository(HealthHubDbContext context): base(context) { }
    }
}