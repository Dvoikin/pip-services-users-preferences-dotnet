using PipServices.Commons.Commands;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.UsersPreferences.Data.Version1;

using System.Threading.Tasks;

namespace PipServices.UsersPreferences.Logic
{
    public interface IUsersPreferencesController
    {
        CommandSet GetCommandSet();

        Task<DataPage<UserPreferencesV1>> GetUsersPreferencesAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<UserPreferencesV1> GetUserPreferencesByIdAsync(string correlationId, string userPreferencesId);
        Task<UserPreferencesV1> SetUserPreferencesAsync(string correlationId, UserPreferencesV1 userPreferences);
        Task<UserPreferencesV1> ClearUserPreferencesAsync(string correlationId, UserPreferencesV1 userPreferences);
        Task<UserPreferencesV1> ClearUsersPreferencesAsync(string correlationId);
    }
}
