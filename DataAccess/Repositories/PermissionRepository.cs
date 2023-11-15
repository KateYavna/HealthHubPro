using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PermissionRepository: Repository<Permission>
    {
        public PermissionRepository(HealthHubDbContext context): base(context) { }
    }
}