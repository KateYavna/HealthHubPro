using AutoMapper;
using DataAccess.Entities;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Constants;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FormService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Person>AuthenticateUser(Login login)
        {
            var user = _unitOfWork.GetPersonByEmailAsync(login.Email).Result;
            Password userPassword = new();
            if (user != null)
            {
                userPassword = await _unitOfWork.PasswordRepository.GetByIdAsync(user.PasswordId);
            }
            if (user != null && VerifyPassword(userPassword, login.Password))
            {
                return user;
            }
            return null;
        }
        public async Task HandleProfileFormAsync(ProfileFormModel model)
        {
            var address = new Address()
            {
                Id = Guid.NewGuid(),
                Street = model.Street,
                City = model.City,
                Country = model.Country,
                ZipCode = model.ZipCode
            };
            await _unitOfWork.AddressRepository.AddAsync(address);

            var emergencyContact = new EmergencyContact()
            {
                Id = Guid.NewGuid(),
                Name = model.EmergencyContactName,
                Relationship = model.EmergencyContactRelationship,
                PhoneNumber = model.EmergencyContactPhoneNumber,
            };
            await _unitOfWork.EmergencyContactRepository.AddAsync(emergencyContact);

            var password = new Password()
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid()
            };
            password.SetPassword(model.Password);
            await _unitOfWork.PasswordRepository.AddAsync(password);

            var person = new Person()
            {
                Id = password.PersonId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                AddressId = address.Id,
                EmergencyContactId = emergencyContact.Id,
                PasswordId = password.Id,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };
            await _unitOfWork.PersonRepository.AddAsync(person);
            var selectedRoles = GetByNamesAsync(model.Roles);
            person.Roles = selectedRoles;
            if (selectedRoles.Select(x => x.Name).Contains(HealthHubConstants.PatientRole))
            {
               var patient = new Patient()
                {
                    Id = Guid.NewGuid(),
                    PersonId = person.Id,
                };
                patient.Allergies = new List<Allergy>();
                patient.Prescriptions = new List<Prescription>();
                patient.Person = person;

                await _unitOfWork.PatientRepository.AddAsync(patient);
            }
            if (selectedRoles.Select(x => x.Name).Contains(HealthHubConstants.HealthcareProviderRole))
            {
                var healthcareProvider = new HealthcareProvider()
                {
                    Id = Guid.NewGuid(),
                    PersonId = person.Id,
                };
                healthcareProvider.Person = person;
                healthcareProvider.Specialties = new List<Specialty>();
                await _unitOfWork.HealthcareProviderRepository.AddAsync(healthcareProvider);
            }
            await _unitOfWork.SaveAsync();
        }

        public List<Person> GetAllPatients()
        {
            var persons = _unitOfWork.PersonRepository.GetAll();
            return persons.Where(x => x.Roles.Select(r => r.Name).Contains(HealthHubConstants.PatientRole)).ToList();
        }

        public List<Person> GetAllHealthcareProviders()
        {
            var persons = _unitOfWork.PersonRepository.GetAll();
            return persons.Where(x => x.Roles.Select(r => r.Name).Contains(HealthHubConstants.HealthcareProviderRole)).ToList();
        }

        public async Task<PatientData> GetPatientDataAsync(string patientId)
        {
            var data = new PatientData();
            data.Person = await _unitOfWork.PersonRepository.GetByIdAsync(new Guid(patientId));

            if (data.Person != null)
            {
                await LoadPatientDataAsync(data);
            }           
            return data;
        }

        public async Task<HealthcareProviderData> GetHealthcareProviderDataAsync(string personId)
        {
            var data = new HealthcareProviderData();
            data.Person = await _unitOfWork.PersonRepository.GetByIdAsync(new Guid(personId));

            if (data.Person != null)
            {
                await LoadHealthcareProviderDataAsync(data);
            }
            return data;
        }
        public async Task AddAppointment(AppointmentPostModel model)
        {
            var appointment = _mapper.Map<Appointment>(model);
            appointment.Id = Guid.NewGuid();          
            await _unitOfWork.AppointmentRepository.AddAsync(appointment);
            appointment.HealthcareProvider = await _unitOfWork.HealthcareProviderRepository.GetByIdAsync(appointment.HealthcareProviderId);
            appointment.Patient = await _unitOfWork.PatientRepository.GetByIdAsync(appointment.PatientId);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAppointment(AppointmentPostModel model)
        {
            var appointment = _mapper.Map<Appointment>(model);
            appointment.Id = model.Id;
            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            appointment.HealthcareProvider = await _unitOfWork.HealthcareProviderRepository.GetByIdAsync(appointment.HealthcareProviderId);
            appointment.Patient = await _unitOfWork.PatientRepository.GetByIdAsync(appointment.PatientId);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddPrescription(PrescriptionPostModel model)
        {
            var prescription = _mapper.Map<Prescription>(model);
            prescription.Id = Guid.NewGuid();
            await _unitOfWork.PrescriptionRepository.AddAsync(prescription);
            prescription.HealthcareProvider = await _unitOfWork.HealthcareProviderRepository.GetByIdAsync(prescription.HealthcareProviderId);
            prescription.PrescriptionHistory = _unitOfWork.PrescriptionHistoryRepository.GetAll()
                .Where(h => h.PrescriptionId == prescription.Id).ToList();
            await _unitOfWork.SaveAsync();
        }

        public async Task EditPrescription(PrescriptionPostModel model)
        {
            var prescription = _mapper.Map<Prescription>(model);
            prescription.Id = model.Id;
            await _unitOfWork.PrescriptionRepository.UpdateAsync(prescription);
            prescription.HealthcareProvider = await _unitOfWork.HealthcareProviderRepository.GetByIdAsync(prescription.HealthcareProviderId);
            prescription.PrescriptionHistory = _unitOfWork.PrescriptionHistoryRepository.GetAll()
              .Where(h => h.PrescriptionId == prescription.Id).ToList();
            await _unitOfWork.SaveAsync();
        }

        public async Task AddPrescriptionHistory(PrescriptionHistory model)
        {
            model.Id = Guid.NewGuid();
            await _unitOfWork.PrescriptionHistoryRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditPrescriptionHistory(PrescriptionHistory model)
        {
            await _unitOfWork.PrescriptionHistoryRepository.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public List<Appointment> GetPatientAppointmentList(AppointmentPostModel appointment) 
        {
            return _unitOfWork.AppointmentRepository.GetAll()
                .Include(a => a.HealthcareProvider)
                .ThenInclude(h => h.Person)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == appointment.PatientId).ToList();
        }

        public List<Prescription> GetPatientPrescriptionList(PrescriptionPostModel prescription)
        {
            return _unitOfWork.PrescriptionRepository.GetAll()
                .Include(a => a.HealthcareProvider)
                .ThenInclude(h => h.Person)
                .Include(a => a.PrescriptionHistory.Where(p => p.IsDeleted == false))
                .Where(a => a.PatientId == prescription.PatientId).ToList();
        }

        public List<PrescriptionHistory> GetPrescriptionHistoryList(PrescriptionHistory prescriptionHistory)
        {
            return _unitOfWork.PrescriptionHistoryRepository.GetAll()
                .Where(a => a.PrescriptionId == prescriptionHistory.PrescriptionId && prescriptionHistory.IsDeleted == false).ToList();
        }
        private List<Role> GetByNamesAsync(List<string> roleNames)
        {
            var allRoles = _unitOfWork.RoleRepository.GetAll();
            return allRoles.Where(x => roleNames.Contains(x.Name)).ToList();
        }
        private async Task LoadPatientDataAsync(PatientData data)
        {
            data.Address = await _unitOfWork.AddressRepository.GetByIdAsync(data.Person.AddressId);
            data.EmergencyContact = await _unitOfWork.EmergencyContactRepository.GetByIdAsync(data.Person.EmergencyContactId);

            var patient = _unitOfWork.PatientRepository.GetAll()
                                .Include(p => p.Prescriptions)
                                .Include(p => p.Allergies)
                                .FirstOrDefault(x => x.PersonId == data.Person.Id);
            
            data.Patient = patient;
            data.HealthcareProviders = _unitOfWork.HealthcareProviderRepository.GetAll()
                               .Include(p => p.Specialties)
                               .Include(p => p.Person).ToList();
            data.CommonAllergies = _unitOfWork.AllergyRepository.GetAll().ToList();
            data.Appointments = _unitOfWork.AppointmentRepository.GetAll()
                .Include(a => a.HealthcareProvider)
                .ThenInclude(h => h.Person)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patient.Id).ToList();
            data.Prescriptions = _unitOfWork.PrescriptionRepository.GetAll()
                .Include(p => p.PrescriptionHistory.Where(h => h.IsDeleted == false))
                .Include(p => p.HealthcareProvider)
                .ThenInclude(h => h.Person)
                .Where(p => p.PatientId == patient.Id).ToList();
        }
        private async Task LoadHealthcareProviderDataAsync(HealthcareProviderData data)
        {
            data.Address = await _unitOfWork.AddressRepository.GetByIdAsync(data.Person.AddressId);
            data.EmergencyContact = await _unitOfWork.EmergencyContactRepository.GetByIdAsync(data.Person.EmergencyContactId);
            data.HealthcareProvider = _unitOfWork.HealthcareProviderRepository.GetAll()
                .Include(h => h.Specialties)
                .Include (h => h.Person)
                .FirstOrDefault(x => x.PersonId == data.Person.Id);
            data.CommonSpecialties = _unitOfWork.SpecialtyRepository.GetAll().ToList();
            data.Appointments = _unitOfWork.AppointmentRepository.GetAll()
                .Include(a => a.Patient)
                .ThenInclude(p => p.Person)
                .Where(a => a.HealthcareProviderId == data.HealthcareProvider.Id).ToList();
        }
        private bool VerifyPassword(Password storedPassword, string enteredPassword)
        {
            var enteredPasswordHash = Password.HashPassword(enteredPassword, storedPassword.Salt);
            return storedPassword.PasswordHash == enteredPasswordHash;
        }
    }
}