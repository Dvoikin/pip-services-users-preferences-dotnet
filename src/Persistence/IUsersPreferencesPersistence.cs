using PipServices.Commons.Data;
using PipServices.Data;
using PipServices.UsersPreferences.Data.Version1;

using System.Threading.Tasks;

namespace PipServices.UsersPreferences.Persistence
{
    public interface IUsersPreferencesPersistence : IGetter<UserPreferencesV1, string>, IWriter<UserPreferencesV1, string>
    {
        Task<DataPage<UserPreferencesV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<UserPreferencesV1> GetOneRandomAsync(string correlationId, FilterParams filterParams);
    }
}