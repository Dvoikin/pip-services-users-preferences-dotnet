using PipServices.Commons.Convert;
using PipServices.Commons.Validate;

namespace PipServices.UsersPreferences.Data.Version1
{
    class UserPreferencesV1Schema : ObjectSchema
    {
        public UserPreferencesV1Schema()
        {
            WithOptionalProperty("id", TypeCode.String);
            WithOptionalProperty("userId", TypeCode.String);
            WithOptionalProperty("preferredEmail", TypeCode.String);
            WithOptionalProperty("timeZone", TypeCode.String);
            WithOptionalProperty("language", TypeCode.String);
            WithOptionalProperty("theme", TypeCode.String);
            WithOptionalProperty("notifications", new ArraySchema(TypeCode.Object));
        }
    }
}
