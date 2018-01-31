using PipServices.Container;
using PipServices.UsersPreferences.Build;

namespace PipServices.UsersPreferences.Container
{
    public class UsersPreferencesProcess : ProcessContainer
    {
        public UsersPreferencesProcess()
            : base("users preferences", "Inspirational users preferences microservice")
        {
            _factories.Add(new UsersPreferencesServiceFactory());
        }
    }
}
