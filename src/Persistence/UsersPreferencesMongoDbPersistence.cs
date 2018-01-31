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
            var search = filterParams.GetAsNullableString("search");

            var id = filterParams.GetAsNullableString("id");
            var userId = filterParams.GetAsNullableString("user_id");
            var PreferredEmail = filterParams.GetAsNullableString("preferred_email");

            var builder = Builders<UserPreferencesV1>.Filter;
            var filter = builder.Empty;
            if (id != null) filter &= builder.Eq(q => q.Id, id);
            if (userId != null) filter &= builder.Eq(q => q.UserId, userId);
            if (PreferredEmail != null) filter &= builder.Eq(q => q.PreferredEmail, PreferredEmail);
            if (!string.IsNullOrEmpty(search))
            {
                var searchFilter = builder.Where(q => q.Id.ToLower().Contains(search));
                searchFilter |= builder.Where(q => q.UserId.ToLower().Contains(search));
                searchFilter |= builder.Where(q => q.PreferredEmail.ToLower().Contains(search));
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
