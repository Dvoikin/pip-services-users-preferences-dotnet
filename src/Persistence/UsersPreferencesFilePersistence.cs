using PipServices.Commons.Config;
using PipServices.Data.File;
using PipServices.UsersPreferences.Data.Version1;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesFilePersistence : UsersPreferencesMemoryPersistence
    {
        protected JsonFilePersister<UserPreferencesV1> _persister;

        public UsersPreferencesFilePersistence()
        {
            _persister = new JsonFilePersister<UserPreferencesV1>();
            _loader = _persister;
            _saver = _persister;
        }

        public override void Configure(ConfigParams config)
        {
            base.Configure(config);

            _persister.Configure(config);
        }
    }
}
