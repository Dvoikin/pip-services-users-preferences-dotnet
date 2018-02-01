using PipServices.Container;
using PipServices.UsersPreferences.Build;

namespace PipServices.UsersPreferences.Container
{
    public class UsersPreferencesProcess : ProcessContainer
    {
        public UsersPreferencesProcess()
            : base("users preferences", "Users preferences microservice")
        {
            _factories.Add(new UsersPreferencesServiceFactory());
        }
    }
}
