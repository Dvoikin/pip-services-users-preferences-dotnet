using PipServices.Commons.Convert;
using PipServices.Commons.Validate;

namespace PipServices.UsersPreferences.Data.Version1
{
    class UserPreferencesV1Schema : ObjectSchema
    {
        public UserPreferencesV1Schema()
        {
            WithOptionalProperty("id", TypeCode.String);
            WithOptionalProperty("user_id", TypeCode.String);
            WithOptionalProperty("preferred_email", TypeCode.String);
            WithOptionalProperty("time_zone", TypeCode.String);
            WithOptionalProperty("language", TypeCode.String);
            WithOptionalProperty("theme", TypeCode.String);
            WithOptionalProperty("notifications", new ArraySchema(TypeCode.Object));
        }
    }
}
