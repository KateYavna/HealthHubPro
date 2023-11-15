using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class RoleRepository: Repository<Role>
    {
        public RoleRepository(HealthHubDbContext context): base(context) { }
    }
}