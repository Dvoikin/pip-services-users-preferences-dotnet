using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using PipServices.Commons.Data;
using PipServices.Data.Memory;
using PipServices.UsersPreferences.Data.Version1;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesMemoryPersistence : IdentifiableMemoryPersistence<UserPreferencesV1, string>, IUsersPreferencesPersistence
    {
        public int ItemsCount { get { return _items.Count; } }

        public Task<UserPreferencesV1> GetOneRandomAsync(string correlationId, FilterParams filterParams)
        {
            return base.GetOneRandomAsync(correlationId, ComposeFilter(filterParams));
        }

        public Task<DataPage<UserPreferencesV1>> GetPageByFilterAsync(string correlationId, FilterParams filterParams, PagingParams paging)
        {
            return base.GetPageByFilterAsync(correlationId, ComposeFilter(filterParams), paging);
        }

        private IList<Func<UserPreferencesV1, bool>> ComposeFilter(FilterParams filter)
        {
            var result = new List<Func<UserPreferencesV1, bool>>();

            filter = filter ?? new FilterParams();

            var search = filter.GetAsNullableString("search");
            var userId = filter.GetAsNullableString("userId");
            var preferredEmail = filter.GetAsNullableString("preferredEmail");

            result.Add(quote => string.IsNullOrWhiteSpace(search) || MatchSearch(quote, search));
            result.Add(quote => string.IsNullOrWhiteSpace(userId) || quote.UserId.Equals(userId));
            result.Add(quote => string.IsNullOrWhiteSpace(preferredEmail) || quote.PreferredEmail.Equals(preferredEmail));

            return result;
        }

        private bool MatchSearch(UserPreferencesV1 item, string search)
        {
            return (item.UserId != null && item.UserId.Contains(search)) ? true
                : (item.PreferredEmail != null && item.PreferredEmail.Contains(search)) ? true
                : false;
        }
    }
}
