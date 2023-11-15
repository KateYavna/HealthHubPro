using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class AppointmentRepository: Repository<Appointment>
    {
        public AppointmentRepository(HealthHubDbContext context): base(context) { }
    }
}