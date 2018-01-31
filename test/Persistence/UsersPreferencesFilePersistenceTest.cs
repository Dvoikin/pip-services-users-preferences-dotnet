using PipServices.Commons.Config;
using System.Threading.Tasks;
using Xunit;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesFilePersistenceTest
    {
        private UsersPreferencesFilePersistence _persistence;
        private UsersPreferencesPersistenceFixture _fixture;

        public UsersPreferencesFilePersistenceTest()
        {
            ConfigParams config = ConfigParams.FromTuples(
                "path", "user-preferences.json"
            );
            _persistence = new UsersPreferencesFilePersistence();
            _persistence.Configure(config);
            _persistence.OpenAsync(null).Wait();
            _persistence.ClearAsync(null).Wait();

            _fixture = new UsersPreferencesPersistenceFixture(_persistence);
        }

        [Fact]
        public async Task TestFileCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestFileGetByFilterAsync()
        {
            await _fixture.TestGetByFilterAsync();
        }
    }
}
