
using PipServices.Commons.Build;
using PipServices.Commons.Refer;
using PipServices.UsersPreferences.Logic;
using PipServices.UsersPreferences.Persistence;
using PipServices.UsersPreferences.Services.Version1;

namespace PipServices.UsersPreferences.Build
{
    class UsersPreferencesServiceFactory : Factory
    {
        public static Descriptor Descriptor = new Descriptor("pip-services-users-preferences", "factory", "default", "default", "1.0");
        public static Descriptor MemoryPersistenceDescriptor = new Descriptor("pip-services-users-preferences", "persistence", "memory", "*", "1.0");
        public static Descriptor FilePersistenceDescriptor = new Descriptor("pip-services-users-preferences", "persistence", "file", "*", "1.0");
        public static Descriptor MongoDbPersistenceDescriptor = new Descriptor("pip-services-users-preferences", "persistence", "mongodb", "*", "1.0");
        public static Descriptor ControllerDescriptor = new Descriptor("pip-services-users-preferences", "controller", "default", "*", "1.0");
        public static Descriptor HttpServiceDescriptor = new Descriptor("pip-services-users-preferences", "service", "http", "*", "1.0");

        public UsersPreferencesServiceFactory()
        {
            RegisterAsType(MemoryPersistenceDescriptor, typeof(UsersPreferencesMemoryPersistence));
            RegisterAsType(FilePersistenceDescriptor, typeof(UsersPreferencesFilePersistence));
            RegisterAsType(MongoDbPersistenceDescriptor, typeof(UsersPreferencesMongoDbPersistence));
            RegisterAsType(ControllerDescriptor, typeof(UsersPreferencesController));
            RegisterAsType(HttpServiceDescriptor, typeof(UsersPreferencesHttpServiceV1));
        }
    }
}
