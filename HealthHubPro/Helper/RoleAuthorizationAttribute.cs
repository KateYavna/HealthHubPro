using HealthHubPro.Helper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HealthHubPro.Helper
{
    public class RoleAuthorizationAttribute: Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        public RoleAuthorizationAttribute(params string[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userProvider = context.HttpContext.RequestServices.GetRequiredService<IUserSessionProvider>();
            var userSession = userProvider.GetCurrentUser();
            if(!_roles.Any(r => userSession.HasRole(r)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
