using System.Threading.Tasks;
using System.Linq;

using PipServices.Commons.Data;
using PipServices.UsersPreferences.Data.Version1;
using PipServices.Data.MongoDb;
using MongoDB.Driver;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesMongoDbPersistence : IdentifiableMongoDbPersistence<UserPreferencesV1, string>, IUsersPreferencesPersistence
    {
        public UsersPreferencesMongoDbPersistence() : base("users_preferences")
        {
        }

        private FilterDefinition<UserPreferencesV1> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var search = filterParams.GetAsNullableString("search");
            var userId = filterParams.GetAsNullableString("user_id");
            var preferredEmail = filterParams.GetAsNullableString("preferred_email");
            var theme = filterParams.GetAsNullableString("theme");
            var language = filterParams.GetAsNullableString("language");

            var builder = Builders<UserPreferencesV1>.Filter;
            var filter = builder.Empty;
            if (userId != null) filter &= builder.Eq(up => up.UserId, userId);
            if (preferredEmail != null) filter &= builder.Eq(up => up.PreferredEmail, preferredEmail);
            if (theme != null) filter &= builder.Eq(up => up.Theme, theme);
            if (language != null) filter &= builder.Eq(up => up.Language, language);
            if (!string.IsNullOrEmpty(search))
            {
                var searchFilter = builder.Where(up => up.UserId.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.PreferredEmail.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.Theme.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.Language.ToLower().Contains(search));
                filter &= searchFilter;
            }

            return filter;
        }

        public Task<DataPage<UserPreferencesV1>> GetPageByFilterAsync(string correlationId, FilterParams filterParams, PagingParams paging)
        {
            return base.GetPageByFilterAsync(correlationId, ComposeFilter(filterParams), paging);
        }

        public new Task<UserPreferencesV1> GetOneByIdAsync(string correlationId, string userId)
        {
            var filterParams = new FilterParams();

            filterParams.Add("user_id", userId);
            return base.GetOneRandomAsync(correlationId, ComposeFilter(filterParams));
        }

        public new Task<UserPreferencesV1> SetAsync(string correlationId, UserPreferencesV1 UserPreferences)
        {
            var up = UserPreferences;
            return base.SetAsync(correlationId, up);
        }

        public Task<UserPreferencesV1> ClearByIdAsync(string correlationId, string UserPreferencesId)
        {
            return base.SetAsync(correlationId, new UserPreferencesV1
            {
                Id = UserPreferencesId,
                UserId = UserPreferencesId
            });
        }

    }
}
