using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.UnitOfWork.Interfaces;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HealthHubDbContext _context;

        public IRepository<Address> AddressRepository {  get; }

        public IRepository<Allergy> AllergyRepository { get; }

        public IRepository<Appointment> AppointmentRepository { get; }

        public IRepository<EmergencyContact> EmergencyContactRepository { get; }

        public IRepository<HealthcareProvider> HealthcareProviderRepository { get; }

        public IRepository<Password> PasswordRepository { get; }

        public IRepository<Patient> PatientRepository { get; }

        public IRepository<Permission> PermissionRepository { get; }

        public IRepository<Person> PersonRepository { get; }

        public IRepository<Prescription> PrescriptionRepository { get; }

        public IRepository<PrescriptionHistory> PrescriptionHistoryRepository { get; }

        public IRepository<Role> RoleRepository { get; }

        public IRepository<Specialty> SpecialtyRepository { get; }
        public UnitOfWork(HealthHubDbContext healthHubDbContext, IRepository<Address> addressRepository,
           IRepository<Allergy> allergyRepository, IRepository<Appointment> appointmentRepository,
           IRepository<EmergencyContact> emergencyContactRepository, IRepository<HealthcareProvider> healthcareProviderRepository,
           IRepository<Password> passwordRepository, IRepository<Patient> patientRepository, 
           IRepository<Permission> permissionRepository, IRepository<Person> personRepository,
           IRepository<Prescription> prescriptionRepository, IRepository<PrescriptionHistory> prescriptionHistoryRepository,
           IRepository<Role> roleRepository, IRepository<Specialty> specialtyRepository)
        {
            _context = healthHubDbContext;
            AddressRepository = addressRepository;
            AllergyRepository = allergyRepository;
            AppointmentRepository = appointmentRepository;
            EmergencyContactRepository = emergencyContactRepository;
            HealthcareProviderRepository = healthcareProviderRepository;
            PasswordRepository = passwordRepository;
            PatientRepository = patientRepository;
            PermissionRepository = permissionRepository;
            PersonRepository = personRepository;
            PrescriptionRepository = prescriptionRepository;
            RoleRepository = roleRepository;
            SpecialtyRepository = specialtyRepository;
            PrescriptionHistoryRepository = prescriptionHistoryRepository;
        }

        public virtual void Dispose(bool disposing)
        {
           if (disposing)
            {
                _context.Dispose();
            }
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}