using esport.Models.Enums;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace esport.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int MatchId { get; set; }
        public string Slug { get; set; }
        public DateTime? BeginDate { get; set; }
        public bool Complete { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Finished { get; set; }
        public bool Forfeight { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MatchStatusType Status { get; set; }
        public string VideoUrl { get; set; }
        public Winner Winner { get; set; }
    }
}