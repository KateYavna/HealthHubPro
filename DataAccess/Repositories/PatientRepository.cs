using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PatientRepository: Repository<Patient>
    {
        public PatientRepository(HealthHubDbContext context): base(context) { }
    }
}