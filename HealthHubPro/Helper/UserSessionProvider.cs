using HealthHubPro.Helper.Interfaces;
using Services.Constants;
using System.Security.Claims;
using System.Text.Json;

namespace HealthHubPro.Helper
{
    public class UserSessionProvider: IUserSessionProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserSessionProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public UserSession GetCurrentUser()
        {
            var claims = _contextAccessor.HttpContext?.User?.Claims;
            var userName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? HealthHubConstants.PatientRole;
            var userRolesClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userRole = userRolesClaim is null ? new string[] { HealthHubConstants.PatientRole } : JsonSerializer.Deserialize<string[]>(userRolesClaim);
            return new UserSession(userName, userRole);
        }
    }
}