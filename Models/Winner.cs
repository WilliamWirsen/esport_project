using esport.Models.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace esport.Models
{
    public class Winner
    {
        public int? WinnerId { get; set; }
        public WinnerType? WinnerType { get; set; }

    }
}