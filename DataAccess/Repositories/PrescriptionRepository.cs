using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PrescriptionRepository: Repository<Prescription>
    {
        public PrescriptionRepository(HealthHubDbContext context): base(context) { }
    }
}