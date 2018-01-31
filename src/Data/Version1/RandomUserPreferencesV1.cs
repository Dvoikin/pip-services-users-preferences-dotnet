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

        public static NotificationPreferenceV1[] Notifications()
        {
            var count = RandomInteger.NextInteger(0, 5);
            var notifications = new NotificationPreferenceV1[count];

            for (var index = 0; index < count; index++)
            {
                notifications[index] = new NotificationPreferenceV1();
                notifications[index].Area = RandomText.Word().ToLower();
                notifications[index].EmailMinSeverity = (MinNotificationSeverityV1) RandomInteger.NextInteger(-1, 3);
                notifications[index].ShowMinSeverity = (MinNotificationSeverityV1) RandomInteger.NextInteger(-1, 3);
            }

            return notifications;
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
                Theme = Theme(),
                Notifications = Notifications()
            };
        }
    }
}
