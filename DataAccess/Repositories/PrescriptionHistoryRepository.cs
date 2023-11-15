using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PrescriptionHistoryRepository: Repository<PrescriptionHistory>
    {
        public PrescriptionHistoryRepository(HealthHubDbContext context): base(context) { }
    }
}