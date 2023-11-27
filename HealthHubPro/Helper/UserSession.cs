namespace HealthHubPro.Helper
{
    public class UserSession
    {
        public UserSession(string name, IReadOnlyList<string>roles)
        {
            Name = name;
            Roles = roles;
        }
        public string Name { get; }
        public IReadOnlyList<string> Roles { get; }
        public bool HasRole(string role)=> Roles.Contains(role, StringComparer.OrdinalIgnoreCase);
    }
}