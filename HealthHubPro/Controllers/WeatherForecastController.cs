using DataAccess.Entities;
using DataAccess.UnitOfWork.Interfaces;
using HealthHubPro.Helper;
using HealthHubPro.Helper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Constants;
using Services.Models;
using Services.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace HealthHubPro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserSession _userSession;
        public WeatherForecastController(IFormService formService, IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
        {          
            _formService = formService;
            _unitOfWork = unitOfWork;
            _userSession = userSessionProvider.GetCurrentUser();
        }
        [HttpPost("auth")]
        public async Task<ActionResult<AuthResponse>> LoginAsync([FromBody] Login login)
        {
            var response = new AuthResponse();
            var user = await _formService.AuthenticateUser(login);
            if (user == null)
                return BadRequest("Unauthorized");

            var roleNames = user.Roles.Select(role => role.Name).ToList();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(roleNames))
            };
            response.Token = CreateToken(claims);
            response.Name = user.Email;
            return Ok(response);
        }

        [HttpPost("registration")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult> HandleProfileForm([FromBody] ProfileFormModel model)
        {
            await _formService.HandleProfileFormAsync(model);
            return Ok("data was saved successfully");
        }

        [HttpPost("appointment")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<List<Appointment>>> AddAppointment([FromBody] AppointmentPostModel model)
        {
            await _formService.AddAppointment(model);
            return _formService.GetPatientAppointmentList(model);
        }

        [HttpPost("prescription")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<List<Prescription>>> AddPrescription([FromBody] PrescriptionPostModel model)
        {
            await _formService.AddPrescription(model);
            return _formService.GetPatientPrescriptionList(model);
        }

        [HttpPost("specialty")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult> AddSpecialty([FromBody] Specialty model)
        {
            await _unitOfWork.SpecialtyRepository.AddAsync(model);
            return Ok("Specialty added");
        }

        [HttpPost("prescriptionHistory")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<List<PrescriptionHistory>>> AddPrescriptionHistory([FromBody] PrescriptionHistory model)
        {
            await _formService.AddPrescriptionHistory(model);
            return _formService.GetPrescriptionHistoryList(model);
        }

        [HttpGet("roles")]
        public ActionResult<List<string>> GetAllRoles()
        {
            return _unitOfWork.RoleRepository.GetAll().Select(x =>x.Name).ToList();
        }
        [HttpGet("patients")]
        public ActionResult<List<Person>> GetAllPatients()
        {
            return _formService.GetAllPatients();
        }
        [HttpGet("healthcareProviders")]
        public ActionResult<List<Person>> GetAllHealthcareProviders()
        {
            return _formService.GetAllHealthcareProviders();
        }
        [HttpGet("patientUpdate/{id}")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole, HealthHubConstants.PatientRole)]
        public async Task<ActionResult<PatientData>> GetPatientData(string id)
        {
            if (await FindPatientPermission(id))
                return await _formService.GetPatientDataAsync(id);
            else
                return BadRequest("You doesn't have permission");
        }

        [HttpGet("healthcareProviderUpdate/{id}")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<HealthcareProviderData>> GetHealthcareProviderData(string id)
        {
            if (await FindHealthcareProviderPermission(id))
                return await _formService.GetHealthcareProviderDataAsync(id);
            else return BadRequest("You doesn't have permission");
        }

        [HttpPut("appointment")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<List<Appointment>>> EditAppointment([FromBody] AppointmentPostModel model)
        {
            await _formService.EditAppointment(model);
            return _formService.GetPatientAppointmentList(model);
        }

        [HttpPut("prescription")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<List<Prescription>>> EditPrescription([FromBody] PrescriptionPostModel model)
        {
            await _formService.EditPrescription(model);
            return _formService.GetPatientPrescriptionList(model);
        }

        [HttpPut("prescriptionHistory")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<ActionResult<List<PrescriptionHistory>>> EditPrescriptionHistory([FromBody] PrescriptionHistory model)
        {
            await _formService.EditPrescriptionHistory(model);
            return _formService.GetPrescriptionHistoryList(model);
        }

        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole, HealthHubConstants.PatientRole)]
        [HttpPut("person")]
        public async Task<ActionResult> EditPerson(Person person)
        {
            if (await FindPatientPermission(person.Id.ToString()))
            {
                await _unitOfWork.PersonRepository.UpdateAsync(person);
                return Ok();
            }              
            else
               return BadRequest("You doesn't have permission");
        }
        [HttpPut("address")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole, HealthHubConstants.PatientRole)]
        public async Task EditAddress(Address address)
        {
            await _unitOfWork.AddressRepository.UpdateAsync(address);
        }
        [HttpPut("emergencyContact")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole, HealthHubConstants.PatientRole)]
        public async Task EditEmergencyContact (EmergencyContact emergencyContact)
        {
            await _unitOfWork.EmergencyContactRepository.UpdateAsync(emergencyContact);
        }
        [HttpPut("healthcareProvider")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<IActionResult> EditHealthcareProvider(HealthcareProvider provider)
        {
            if (provider == null || provider.Id == Guid.Empty)
            {
                return BadRequest("invalid provider data");
            }
            var existingProvider = await _unitOfWork.HealthcareProviderRepository.GetByIdAsync(provider.Id);
            if (existingProvider == null)
            {
                return NotFound("Provider not found");
            }
            existingProvider.Specialties = provider.Specialties;
            existingProvider.Person = provider.Person;
            try 
            {
                if (await FindHealthcareProviderPermission(existingProvider.Id.ToString()))
                {
                    await _unitOfWork.HealthcareProviderRepository.UpdateAsync(existingProvider);
                    return Ok("Provider updated successfully");
                }
                else
                    return BadRequest("You don't have permission");
                
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("patient")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<IActionResult> EditPatient (Patient patient)
        {
            if (patient == null || patient.Id == Guid.Empty)
            {
                return BadRequest("Invalid patient data");
            }

            var existingPatient = await _unitOfWork.PatientRepository.GetByIdAsync(patient.Id);

            if (existingPatient == null)
            {
                return NotFound("Patient not found");
            }

            existingPatient.Allergies = patient.Allergies;
            existingPatient.Prescriptions = patient.Prescriptions;

            try
            {
                await _unitOfWork.PatientRepository.UpdateAsync(existingPatient);
                return Ok("Patient allergies updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("appointment/{id}")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            await _unitOfWork.AppointmentRepository.SoftDeleteAsync(new Guid(id));
            return Ok("appointment was deleted.");
        }

        [HttpDelete("prescription/{id}")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<IActionResult> DeletePrescription(string id)
        {
            await _unitOfWork.PrescriptionRepository.SoftDeleteAsync(new Guid(id));
            return Ok("prescription was deleted.");
        }

        [HttpDelete("prescriptionHistory/{id}")]
        [RoleAuthorization(HealthHubConstants.HealthcareProviderRole)]
        public async Task<IActionResult> DeletePrescriptionHistory(string id)
        {
            await _unitOfWork.PrescriptionHistoryRepository.SoftDeleteAsync(new Guid(id));
            return Ok("prescription history was deleted.");
        }

        private static string CreateToken (List<Claim>claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(HealthHubConstants.Minutes15)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private async Task<bool> FindPatientPermission(string id)
        {
            string[] roles = _userSession.Roles.ToArray();
            var person = await _unitOfWork.GetPersonByEmailAsync(_userSession.Name);
            return roles.Contains(HealthHubConstants.HealthcareProviderRole) ||
                (person != null && roles.Contains(HealthHubConstants.PatientRole) && person.Id == new Guid(id));
        }
        private async Task<bool> FindHealthcareProviderPermission(string id)
        {
            string[] roles = _userSession.Roles.ToArray();
            var person = await _unitOfWork.GetPersonByEmailAsync(_userSession.Name);
            return roles.Contains(HealthHubConstants.HealthcareProviderRole) && person != null && person.Id == new Guid(id);
        }
    }
}