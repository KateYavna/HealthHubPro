using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class AllergyRepository: Repository<Allergy>
    {
        public AllergyRepository(HealthHubDbContext context): base(context) { }      
    }
}