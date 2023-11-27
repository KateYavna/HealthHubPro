using DataAccess.Entities;
using Services.Models;

namespace Services.Services.Interfaces
{
    public interface IFormService
    {
        Task HandleProfileFormAsync(ProfileFormModel model);
        List<Person> GetAllPatients();
        List<Person> GetAllHealthcareProviders();
        Task<PatientData> GetPatientDataAsync(string patientId);
        Task<HealthcareProviderData> GetHealthcareProviderDataAsync(string personId);
        Task AddAppointment(AppointmentPostModel model);
        Task EditAppointment(AppointmentPostModel model);
        Task AddPrescription(PrescriptionPostModel model);
        Task EditPrescription(PrescriptionPostModel model);
        Task AddPrescriptionHistory(PrescriptionHistory model);
        Task EditPrescriptionHistory(PrescriptionHistory model);
        List<Appointment> GetPatientAppointmentList(AppointmentPostModel appointment);
        List<Prescription> GetPatientPrescriptionList(PrescriptionPostModel prescription);
        List<PrescriptionHistory> GetPrescriptionHistoryList(PrescriptionHistory prescriptionHistory);
        Task<Person> AuthenticateUser(Login login);
    }
}