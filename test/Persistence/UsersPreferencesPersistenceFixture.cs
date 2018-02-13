using PipServices.Commons.Data;
using PipServices.UsersPreferences.Data.Version1;
using System.Threading.Tasks;
using Xunit;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesPersistenceFixture
    {
        private static UserPreferencesV1 USER_PREFERENCES1 = CreateUserPreferences("1", "1", "green");
        private static UserPreferencesV1 USER_PREFERENCES2 = CreateUserPreferences("2", "2", "blue");
        private static UserPreferencesV1 USER_PREFERENCES3 = CreateUserPreferences("3", "3", "blue");

        private IUsersPreferencesPersistence _persistence;

        public UsersPreferencesPersistenceFixture(IUsersPreferencesPersistence persistence)
        {
            _persistence = persistence;
        }

        private static UserPreferencesV1 CreateUserPreferences(string id, string userId, string theme)
        {
            var userPreferences = RandomUserPreferencesV1.UserPreferences();
            userPreferences.Id = id;
            userPreferences.UserId = userId;
            userPreferences.Theme = theme;
            return userPreferences;
        }

        public async Task TestCrudOperationsAsync()
        {
            // Create one userPreferences
            UserPreferencesV1 userPreferences1 = await _persistence.SetAsync(null, USER_PREFERENCES1);

            Assert.NotNull(userPreferences1);
            Assert.Equal(USER_PREFERENCES1.Id, userPreferences1.Id);
            Assert.Equal(USER_PREFERENCES1.UserId, userPreferences1.UserId);

            // Create another userPreferences
            UserPreferencesV1 userPreferences2 = await _persistence.SetAsync(null, USER_PREFERENCES2);

            Assert.NotNull(userPreferences2);
            Assert.Equal(USER_PREFERENCES2.Id, userPreferences2.Id);
            Assert.Equal(USER_PREFERENCES2.UserId, userPreferences2.UserId);

            // Create another userPreferences
            UserPreferencesV1 userPreferences3 = await _persistence.SetAsync(null, USER_PREFERENCES3);

            Assert.NotNull(userPreferences3);
            Assert.Equal(USER_PREFERENCES3.Id, userPreferences3.Id);
            Assert.Equal(USER_PREFERENCES3.UserId, userPreferences3.UserId);

            // Get all quotes
            DataPage<UserPreferencesV1> page = await _persistence.GetPageByFilterAsync(null, null, null);
            Assert.NotNull(page);
            Assert.NotNull(page.Data);
            Assert.Equal(3, page.Data.Count);

            // Update the userPreferences
            userPreferences1.UserId = "3";
            UserPreferencesV1 userPreferences = await _persistence.SetAsync(
                null,
                userPreferences1
            );

            Assert.NotNull(userPreferences);
            Assert.Equal(userPreferences1.Id, userPreferences.Id);
            Assert.Equal("3", userPreferences.UserId);

            // Delete the quote
            userPreferences = await _persistence.ClearByIdAsync(null, userPreferences1.UserId);

            Assert.Null(userPreferences.Theme);
        }

        public async Task TestGetByFilterAsync()
        {
            // Create items
            await _persistence.SetAsync(null, USER_PREFERENCES1);
            await _persistence.SetAsync(null, USER_PREFERENCES2);
            await _persistence.SetAsync(null, USER_PREFERENCES3);

            // Get by id
            FilterParams filter = FilterParams.FromTuples("user_id", "1");
            DataPage<UserPreferencesV1> page = await _persistence.GetPageByFilterAsync(null, filter, null);
            Assert.Single(page.Data);

            // Get by status
            filter = FilterParams.FromTuples("theme", "blue");
            page = await _persistence.GetPageByFilterAsync(null, filter, null);
            Assert.Equal(2, page.Data.Count);

            // Get by search
            filter = FilterParams.FromTuples("search", USER_PREFERENCES1.PreferredEmail);
            page = await _persistence.GetPageByFilterAsync(null, filter, null);
            Assert.Single(page.Data);
        }
    }
}
