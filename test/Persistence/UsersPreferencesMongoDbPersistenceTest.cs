using PipServices.Commons.Config;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesMongoDbPersistenceTest : IDisposable
    {
        private UsersPreferencesMongoDbPersistence _persistence;
        private UsersPreferencesPersistenceFixture _fixture;

        public UsersPreferencesMongoDbPersistenceTest()
        {
            var MONGODB_COLLECTION = Environment.GetEnvironmentVariable("MONGODB_COLLECTION") ?? "test_users_preferences";
            var MONGODB_SERVICE_URI = Environment.GetEnvironmentVariable("MONGODB_SERVICE_URI") ?? "mongodb://localhost:27017/test";

            var config = ConfigParams.FromTuples(
                "collection", MONGODB_COLLECTION,
                "connection.uri", MONGODB_SERVICE_URI
            );

            _persistence = new UsersPreferencesMongoDbPersistence();
            _persistence.Configure(config);
            _persistence.OpenAsync(null).Wait();
            _persistence.ClearAsync(null).Wait();

            _fixture = new UsersPreferencesPersistenceFixture(_persistence);
        }

        public void Dispose()
        {
            _persistence.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task TestMongoDbCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestMongoDbGetByFilterAsync()
        {
            await _fixture.TestGetByFilterAsync();
        }
    }
}
