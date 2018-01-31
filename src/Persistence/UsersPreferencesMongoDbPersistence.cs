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
        public UsersPreferencesMongoDbPersistence() : base("users-preferences")
        {
        }

        private FilterDefinition<UserPreferencesV1> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var search = filterParams.GetAsNullableString("search");
            var id = filterParams.GetAsNullableString("id");
            var userId = filterParams.GetAsNullableString("user_id");
            var preferredEmail = filterParams.GetAsNullableString("preferred_email");
            var theme = filterParams.GetAsNullableString("theme");
            var language = filterParams.GetAsNullableString("language");

            var builder = Builders<UserPreferencesV1>.Filter;
            var filter = builder.Empty;
            if (id != null) filter &= builder.Eq(up => up.Id, id);
            if (userId != null) filter &= builder.Eq(up => up.UserId, userId);
            if (preferredEmail != null) filter &= builder.Eq(up => up.PreferredEmail, preferredEmail);
            if (theme != null) filter &= builder.Eq(up => up.Theme, theme);
            if (language != null) filter &= builder.Eq(up => up.Language, language);
            if (!string.IsNullOrEmpty(search))
            {
                var searchFilter = builder.Where(up => up.Id.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.UserId.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.PreferredEmail.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.Theme.ToLower().Contains(search));
                searchFilter |= builder.Where(up => up.Language.ToLower().Contains(search));
                filter &= searchFilter;
            }

            return filter;
        }

        public Task<UserPreferencesV1> GetOneRandomAsync(string correlationId, FilterParams filterParams)
        {
            return base.GetOneRandomAsync(correlationId, ComposeFilter(filterParams));
        }

        public Task<DataPage<UserPreferencesV1>> GetPageByFilterAsync(string correlationId, FilterParams filterParams, PagingParams paging)
        {
            return base.GetPageByFilterAsync(correlationId, ComposeFilter(filterParams), paging);
        }

    }
}
