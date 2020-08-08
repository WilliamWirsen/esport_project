using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace esport.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WinnerType
    {
        [EnumMember(Value = "Team")]
        Team,
        [EnumMember(Value = "Player")]
        Player,
    }
}
