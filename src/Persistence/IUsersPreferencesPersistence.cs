using PipServices.Commons.Data;
using PipServices.Data;
using PipServices.UsersPreferences.Data.Version1;

using System.Threading.Tasks;

namespace PipServices.UsersPreferences.Persistence
{
    public interface IUsersPreferencesPersistence : ISetter<UserPreferencesV1>
    {
        Task<DataPage<UserPreferencesV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<UserPreferencesV1> GetOneRandomAsync(string correlationId, FilterParams filterParams);
        Task<UserPreferencesV1> GetOneByIdAsync(string correlationId, string UserPreferencesId);
        Task<UserPreferencesV1> ClearAsync(string correlationId, UserPreferencesV1 UserPreferences);
    }
}