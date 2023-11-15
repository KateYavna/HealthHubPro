using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PasswordRepository: Repository<Password>
    {
        public PasswordRepository(HealthHubDbContext context): base(context) { }
    }
}