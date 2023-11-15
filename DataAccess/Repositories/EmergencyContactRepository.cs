using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class EmergencyContactRepository: Repository<EmergencyContact>
    {
        public EmergencyContactRepository(HealthHubDbContext context): base(context) { }
    }
}