using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Address> AddressRepository { get; }
        IRepository<Allergy> AllergyRepository { get; }
        IRepository<Appointment> AppointmentRepository { get; }
        IRepository<EmergencyContact> EmergencyContactRepository { get; }
        IRepository<HealthcareProvider> HealthcareProviderRepository { get; }
        IRepository<Password> PasswordRepository { get; }
        IRepository<Patient> PatientRepository { get; }
        IRepository<Permission> PermissionRepository { get; }
        IRepository<Person> PersonRepository { get; }
        IRepository<Prescription> PrescriptionRepository { get; }
        IRepository<PrescriptionHistory> PrescriptionHistoryRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<Specialty> SpecialtyRepository { get; }

        Task<Person> GetPersonByEmailAsync(string email);
        Task SaveAsync();
    }
}