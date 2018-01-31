using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.UsersPreferences.Data.Version1;
using PipServices.UsersPreferences.Persistence;
using System.Threading.Tasks;
using Xunit;

namespace PipServices.UsersPreferences.Logic
{
    public class UsersPreferencesControllerTest
    {
        private static UserPreferencesV1 USER_PREFERENCES1 = CreateUserPreferences("1", "1");
        private static UserPreferencesV1 USER_PREFERENCES2 = CreateUserPreferences("2", "2");

        private UsersPreferencesMemoryPersistence _persistence;
        private UsersPreferencesController _controller;

        public UsersPreferencesControllerTest()
        {
            _persistence = new UsersPreferencesMemoryPersistence();
            _controller = new UsersPreferencesController();

            var references = References.FromTuples(
                new Descriptor("pip-services-users-preferences", "persistence", "memory", "default", "1.0"), _persistence
            );
            _controller.SetReferences(references);
        }

        private static UserPreferencesV1 CreateUserPreferences(string id, string userId)
        {
            var userPreferences = RandomUserPreferencesV1.UserPreferences();
            userPreferences.Id = id;
            userPreferences.UserId = userId;
            return userPreferences;
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            // Create one userPreferences
            UserPreferencesV1 userPreferences1 = await _persistence.CreateAsync(null, USER_PREFERENCES1);

            Assert.NotNull(userPreferences1);
            Assert.Equal(USER_PREFERENCES1.Id, userPreferences1.Id);
            Assert.Equal(USER_PREFERENCES1.Notifications.Length, userPreferences1.Notifications.Length);
            Assert.Equal(USER_PREFERENCES1.UserId, userPreferences1.UserId);

            // Create another userPreferences
            UserPreferencesV1 userPreferences2 = await _persistence.CreateAsync(null, USER_PREFERENCES2);

            Assert.NotNull(userPreferences2);
            Assert.Equal(USER_PREFERENCES2.Id, userPreferences2.Id);
            Assert.Equal(USER_PREFERENCES2.Notifications.Length, userPreferences2.Notifications.Length);
            Assert.Equal(USER_PREFERENCES2.UserId, userPreferences2.UserId);

            // Get all userPreferencess
            DataPage<UserPreferencesV1> page = await _persistence.GetPageByFilterAsync(null, null, null);
            Assert.NotNull(page);
            Assert.NotNull(page.Data);
            Assert.Equal(2, page.Data.Count);

            // Update the userPreferences
            userPreferences1.UserId = "3";
            UserPreferencesV1 userPreferences = await _persistence.UpdateAsync(
                null,
                userPreferences1
            );

            Assert.NotNull(userPreferences);
            Assert.Equal(userPreferences1.Id, userPreferences.Id);
            Assert.Equal(userPreferences1.Notifications.Length, userPreferences.Notifications.Length);
            Assert.Equal("3", userPreferences.UserId);

            // Delete the userPreferences
            await _persistence.DeleteByIdAsync(null, userPreferences1.Id);

            // Try to get deleted userPreferences
            userPreferences = await _persistence.GetOneByIdAsync(null, userPreferences1.Id);
            Assert.Null(userPreferences);
        }

    }
}
