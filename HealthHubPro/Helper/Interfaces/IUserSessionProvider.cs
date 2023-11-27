namespace HealthHubPro.Helper.Interfaces
{
    public interface IUserSessionProvider
    {
        public UserSession GetCurrentUser();
    }
}