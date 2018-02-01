using PipServices.Commons.Config;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.UsersPreferences.Data.Version1;
using PipServices.UsersPreferences.Logic;
using PipServices.UsersPreferences.Persistence;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PipServices.UsersPreferences.Services.Version1
{
    public class UsersPreferencesHttpServiceV1Test: IDisposable
    {
        private static UserPreferencesV1 USER_PREFERENCES1 = CreateUserPreferences("1", "1");
        private static UserPreferencesV1 USER_PREFERENCES2 = CreateUserPreferences("2", "2");

        private UsersPreferencesMemoryPersistence _persistence;
        private UsersPreferencesController _controller;
        private UsersPreferencesHttpServiceV1 _service;

        public UsersPreferencesHttpServiceV1Test()
        {
            _persistence = new UsersPreferencesMemoryPersistence();
            _controller = new UsersPreferencesController();

            var config = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", "3000"
            );
            _service = new UsersPreferencesHttpServiceV1();
            _service.Configure(config);

            var references = References.FromTuples(
                new Descriptor("pip-services-users-preferences", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("pip-services-users-preferences", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("pip-services-users-preferences", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.SetReferences(references);
            //_service.OpenAsync(null).Wait();

            // Todo: This is defect! Open shall not block the tread
            Task.Run(() => _service.OpenAsync(null));
            Thread.Sleep(1000); // Just let service a sec to be initialized
        }

        public void Dispose()
        {
            _service.CloseAsync(null).Wait();
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
            UserPreferencesV1 userPreferences1 = await Invoke<UserPreferencesV1>("/users_preferences/set_user_preferences", new { user_preferences = USER_PREFERENCES1 });

            Assert.NotNull(userPreferences1);
            Assert.Equal(USER_PREFERENCES1.Id, userPreferences1.Id);
            Assert.Equal(USER_PREFERENCES1.UserId, userPreferences1.UserId);

            // Create another userPreferences
            UserPreferencesV1 userPreferences2 = await Invoke<UserPreferencesV1>("/users_preferences/set_user_preferences", new { user_preferences = USER_PREFERENCES2 });

            Assert.NotNull(userPreferences2);
            Assert.Equal(USER_PREFERENCES2.Id, userPreferences2.Id);
            Assert.Equal(USER_PREFERENCES2.UserId, userPreferences2.UserId);

            // Get all usersPreferences
            DataPage<UserPreferencesV1> page = await Invoke<DataPage<UserPreferencesV1>>("/users_preferences/get_user_preferences", new { });
            Assert.NotNull(page);
            Assert.NotNull(page.Data);
            Assert.Equal(2, page.Data.Count);

            // Update the userPreferences
            userPreferences1.UserId = "3";
            UserPreferencesV1 userPreferences = await Invoke<UserPreferencesV1>("/users_preferences/set_user_preferences", new { user_preferences = userPreferences1 });

            Assert.NotNull(userPreferences);
            Assert.Equal(userPreferences1.Id, userPreferences.Id);
            Assert.Equal("3", userPreferences.UserId);

            // Clear the userPreferences
            userPreferences = await Invoke<UserPreferencesV1>("/users_preferences/clear_user_preferences", new { user_preferences = userPreferences2 });

            Assert.Null(userPreferences.Theme);
        }

        private static async Task<T> Invoke<T>(string route, dynamic request)
        {
            using (var httpClient = new HttpClient())
            {
                var requestValue = JsonConverter.ToJson(request);
                using (var content = new StringContent(requestValue, Encoding.UTF8, "application/json"))
                {
                    var response = await httpClient.PostAsync("http://localhost:3000" + route, content);
                    var responseValue = response.Content.ReadAsStringAsync().Result;
                    return JsonConverter.FromJson<T>(responseValue);
                }
            }
        }

    }
}
