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

        public Task<DataPage<UserPreferencesV1>> GetPageByFilterAsync(string correlationId, FilterParams filterParams, PagingParams paging)
        {
            return base.GetPageByFilterAsync(correlationId, ComposeFilter(filterParams), paging);
        }

        public Task<UserPreferencesV1> ClearAsync(string correlationId, UserPreferencesV1 UserPreferences)
        {
            return base.SetAsync(correlationId, new UserPreferencesV1 {
                Id = UserPreferences.Id,
                UserId = UserPreferences.UserId,
                PreferredEmail = null,
                TimeZone = null,
                Language = null,
                Theme = null
            });
        }

        private IList<Func<UserPreferencesV1, bool>> ComposeFilter(FilterParams filter)
        {
            var result = new List<Func<UserPreferencesV1, bool>>();

            filter = filter ?? new FilterParams();

            var search = filter.GetAsNullableString("search");
            var userId = filter.GetAsNullableString("user_id");
            var preferredEmail = filter.GetAsNullableString("preferred_email");
            var theme = filter.GetAsNullableString("theme");
            var language = filter.GetAsNullableString("language");

            result.Add(userPreferences => string.IsNullOrWhiteSpace(search) || MatchSearch(userPreferences, search));
            result.Add(userPreferences => string.IsNullOrWhiteSpace(userId) || userPreferences.UserId.Equals(userId));
            result.Add(userPreferences => string.IsNullOrWhiteSpace(preferredEmail) || userPreferences.PreferredEmail.Equals(preferredEmail));
            result.Add(userPreferences => string.IsNullOrWhiteSpace(theme) || userPreferences.Theme.Equals(theme));
            result.Add(userPreferences => string.IsNullOrWhiteSpace(language) || userPreferences.Theme.Equals(language));

            return result;
        }

        private bool MatchSearch(UserPreferencesV1 item, string search)
        {
            return (item.UserId != null && item.UserId.Contains(search)) ? true
                : (item.PreferredEmail != null && item.PreferredEmail.Contains(search)) ? true
                : (item.Theme != null && item.Theme.Contains(search)) ? true
                : (item.Language != null && item.Language.Contains(search)) ? true
                : false;
        }
    }
}
