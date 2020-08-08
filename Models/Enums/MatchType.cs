using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace esport.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchType
    {
        [EnumMember(Value = "best_of")]
        best_of,
        [EnumMember(Value = "custom")]
        custom,
        [EnumMember(Value = "first_to")]
        first_to,
        [EnumMember(Value = "ow_best_of")]
        ow_best_of
    }
}
