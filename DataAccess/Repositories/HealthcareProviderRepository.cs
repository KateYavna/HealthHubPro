using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class HealthcareProviderRepository: Repository<HealthcareProvider>
    {
        public HealthcareProviderRepository(HealthHubDbContext context): base(context) { }
    }
}