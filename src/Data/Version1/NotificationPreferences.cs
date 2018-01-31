using PipServices.Commons.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PipServices.UsersPreferences.Data.Version1
{
    [DataContract]
    public class NotificationPreferenceV1
    {
        public string Area { get; set; }
        public MinNotificationSeverityV1 ShowMinSeverity { get; set; }
        public MinNotificationSeverityV1 EmailMinSeverity { get; set; }
    }
}
