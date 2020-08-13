using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace esport.Models.Enums
{
    public enum MatchStatusType
    {
        [EnumMember(Value = "unknown")]
        [Description("Okänd")]
        unknown,
        [Description("Cancelled")]
        [EnumMember(Value = "canceled")]
        cancelled,
        [Description("Finished")]
        [EnumMember(Value = "finished")]
        finished,
        [Description("Not started")]
        [EnumMember(Value = "not_started")]
        not_started,
        [Description("Postponed")]
        [EnumMember(Value = "postponed")]
        postponed,
        [Description("Running")]
        [EnumMember(Value = "running")]
        running
    }
}
