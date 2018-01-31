using System.Threading.Tasks;
using Xunit;

namespace PipServices.UsersPreferences.Persistence
{
    public class UsersPreferencesMemoryPersistenceTest
    {
        private UsersPreferencesMemoryPersistence _persistence;
        private UsersPreferencesPersistenceFixture _fixture;

        public UsersPreferencesMemoryPersistenceTest()
        {
            _persistence = new UsersPreferencesMemoryPersistence();
            _fixture = new UsersPreferencesPersistenceFixture(_persistence);
        }

        [Fact]
        public async Task TestMemoryCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestMemoryGetByFilterAsync()
        {
            await _fixture.TestGetByFilterAsync();
        }
    }
}
