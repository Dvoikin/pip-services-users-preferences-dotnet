using PipServices.Commons.Commands;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;
using PipServices.Commons.Run;
using PipServices.Commons.Validate;
using PipServices.UsersPreferences.Data.Version1;

namespace PipServices.UsersPreferences.Logic
{
    public class UsersPreferencesCommandSet : CommandSet
    {
        private IUsersPreferencesController _logic;

        public UsersPreferencesCommandSet(IUsersPreferencesController logic)
        {
            _logic = logic;

            AddCommand(MakeGetUsersPreferencesCommand());
            AddCommand(MakeGetUserPreferencesByIdCommand());
            AddCommand(MakeSetUserPreferencesCommand());
            AddCommand(MakeClearUserPreferencesByIdCommand());
        }

        private ICommand MakeGetUsersPreferencesCommand()
        {
            return new Command(
                "get_user_preferences",
                new ObjectSchema()
                    .WithOptionalProperty("filter", new FilterParamsSchema())
                    .WithOptionalProperty("paging", new PagingParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var filter = FilterParams.FromValue(parameters.Get("filter"));
                    var paging = PagingParams.FromValue(parameters.Get("paging"));
                    return await _logic.GetUsersPreferencesAsync(correlationId, filter, paging);
                });
        }

        private ICommand MakeGetUserPreferencesByIdCommand()
        {
            return new Command(
                "get_user_preferences_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("user_preferences_id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var UserPreferencesId = parameters.GetAsString("user_preferences_id");
                    return await _logic.GetUserPreferencesByIdAsync(correlationId, UserPreferencesId);
                });
        }

        private ICommand MakeSetUserPreferencesCommand()
        {
            return new Command(
                "set_user_preferences",
                new ObjectSchema()
                    .WithRequiredProperty("user_preferences", new UserPreferencesV1Schema()),
                async (correlation_id, parameters) =>
                {
                    var UserPreferences = ConvertToUserPreferences(parameters.Get("user_preferences"));
                    return await _logic.SetUserPreferencesAsync(correlation_id, UserPreferences);
                });
        }

        private ICommand MakeClearUserPreferencesByIdCommand()
        {
            return new Command(
                "clear_user_preferences",
                new ObjectSchema()
                    .WithRequiredProperty("user_preferences", new UserPreferencesV1Schema()),
                async (correlationId, parameters) =>
                {
                    var UserPreferences = ConvertToUserPreferences(parameters.Get("user_preferences"));
                    return await _logic.ClearUserPreferencesAsync(correlationId, UserPreferences);
                });
        }

        private static UserPreferencesV1 ConvertToUserPreferences(object value)
        {
            var json = JsonConverter.ToJson(value);
            return JsonConverter.FromJson<UserPreferencesV1>(json);
        }

        //private static UserPreferencesV1 ExtractUserPreferences(Parameters args)
        //{
        //    var map = args.GetAsMap("user_preferences");

        //    return ExtractUserPreferences(map);
        //}

        //private static UserPreferencesV1 ExtractUserPreferences(AnyValueMap map)
        //{
        //    var Id = map.GetAsStringWithDefault("id", string.Empty);
        //    var UserId = map.GetAsStringWithDefault("user_id", string.Empty);
        //    var PreferredEmail = map.GetAsStringWithDefault("preferred_email", string.Empty);
        //    var TimeZone = map.GetAsStringWithDefault("time_zone", string.Empty);
        //    var Language = map.GetAsStringWithDefault("language", string.Empty);
        //    var Theme = map.GetAsStringWithDefault("theme", string.Empty);

        //    return new UserPreferencesV1(Id, UserId, PreferredEmail, TimeZone, Language, Theme);
        //}
    }
}
