using PipServices.Commons.Log;
using PipServices.Commons.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PipServices.UsersPreferences.Data.Version1
{
    [DataContract]
    public class UserPreferencesV1 : IStringIdentifiable
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "preferred_email")]
        public string PreferredEmail { get; set; }

        [DataMember(Name = "time_zone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "theme")]
        public string Theme { get; set; }
    }
}
