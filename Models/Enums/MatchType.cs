using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace esport.Models.Enums
{
    public enum MatchType
    {
        [EnumMember(Value = "best_of")]
        [Description("Best of")]
        best_of,
        [EnumMember(Value = "custom")]
        [Description("Custom")]
        custom,
        [EnumMember(Value = "first_to")]
        [Description("First to")]
        first_to,
        [EnumMember(Value = "ow_best_of")]
        [Description("Overtime | Best of")]
        ow_best_of
    }
}
