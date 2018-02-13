using PipServices.Commons.Data;
using PipServices.Commons.Random;
using System.Collections.Generic;

namespace PipServices.UsersPreferences.Data.Version1
{
    public static class RandomUserPreferencesV1
    {
        public static string PrefferdEmail()
        {
            var value = RandomText.Text(5, 15) + "@" + RandomText.Text(2, 5) + ".com";
            return value;
        }

        public static string TimeZone()
        {
            var value = "UTC+" + RandomInteger.NextInteger(-11, 12).ToString();
            return value;
        }

        public static string Language()
        {
            var value = RandomInteger.NextInteger(1) > 0 ? "ru" : "en";
            return value;
        }

        public static string Theme()
        {
            var value = RandomInteger.NextInteger(1) > 0 ? "green" : "blue";
            return value;
        }

        public static UserPreferencesV1 UserPreferences()
        {
            return new UserPreferencesV1
            {
                Id = IdGenerator.NextLong(),
                UserId = IdGenerator.NextLong(),
                PreferredEmail = PrefferdEmail(),
                TimeZone = TimeZone(),
                Language = Language(),
                Theme = Theme()
            };
        }
    }
}
