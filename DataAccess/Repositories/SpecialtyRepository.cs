using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class SpecialtyRepository: Repository<Specialty>
    {
        public SpecialtyRepository(HealthHubDbContext context): base(context) { }
    }
}