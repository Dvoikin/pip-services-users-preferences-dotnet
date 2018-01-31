using System.Threading.Tasks;

using PipServices.Commons.Commands;
using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.UsersPreferences.Data.Version1;
using PipServices.UsersPreferences.Persistence;

namespace PipServices.UsersPreferences.Logic
{
    public class UsersPreferencesController : IConfigurable, ICommandable, IUsersPreferencesController
    {
        private static ConfigParams _defaultConfig = ConfigParams.FromTuples("dependencies.persistence", "pip-services-users-preferences:persistence:*:*:1.0");

        private DependencyResolver _dependencyResolver = new DependencyResolver(_defaultConfig);
        private IUsersPreferencesPersistence _persistence;
        private UsersPreferencesCommandSet _commandSet;

        public void Configure(ConfigParams config)
        {
            _dependencyResolver.Configure(config);
        }

        public void SetReferences(IReferences references)
        {
            _dependencyResolver.SetReferences(references);
            _persistence = _dependencyResolver.GetOneRequired<IUsersPreferencesPersistence>("persistence");
        }
        
        public CommandSet GetCommandSet()
        {
            return _commandSet ?? (_commandSet = new UsersPreferencesCommandSet(this));
        }
        
        public Task<DataPage<UserPreferencesV1>> GetUsersPreferencesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return _persistence.GetPageByFilterAsync(correlationId, filter, paging);
        }

        public Task<UserPreferencesV1> GetRandomUserPreferencesAsync(string correlationId, FilterParams filter)
        {
            return _persistence.GetOneRandomAsync(correlationId, filter);
        }

        public Task<UserPreferencesV1> GetUserPreferencesByIdAsync(string correlationId, string UserPreferencesId)
        {
            return _persistence.GetOneByIdAsync(correlationId, UserPreferencesId);
        }

        public Task<UserPreferencesV1> CreateUserPreferencesAsync(string correlationId, UserPreferencesV1 UserPreferences)
        {
            return _persistence.CreateAsync(correlationId, UserPreferences);
        }

        public Task<UserPreferencesV1> UpdateUserPreferencesAsync(string correlationId, UserPreferencesV1 UserPreferences)
        {
            return _persistence.UpdateAsync(correlationId, UserPreferences);
        }

        public Task<UserPreferencesV1> DeleteUserPreferencesByIdAsync(string correlationId, string UserPreferencesId)
        {
            return _persistence.DeleteByIdAsync(correlationId, UserPreferencesId);
        }
    }
}
