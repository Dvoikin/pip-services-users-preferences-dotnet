using PipServices.Commons.Refer;
using PipServices.Net.Rest;

namespace PipServices.UsersPreferences.Services.Version1
{
    public class UsersPreferencesHttpServiceV1 : CommandableHttpService
    {
        public UsersPreferencesHttpServiceV1()
            : base("users_preferences")
        {
            _dependencyResolver.Put("controller", new Descriptor("pip-services-users-preferences", "controller", "default", "*", "1.0"));
        }
    }
}
