using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace esport.Models.Enums
{
    public enum WinnerType
    {
        [EnumMember(Value = "Unset")]
        [Description("Unset")]
        Unset,
        [Description("Team")]
        [EnumMember(Value = "Team")]
        Team,
        [Description("Player")]
        [EnumMember(Value = "Player")]
        Player,
    }
}
