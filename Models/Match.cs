using esport.Models.Enums;
using System;
using System.Collections.Generic;

namespace esport.Models
{
    public class Match
    {
        public int Id { get; set; }
        public bool Draw { get; set; }
        public bool Forfeit { get; set; }
        public string Name { get; set; }
        public int NumberOfGames { get; set; }
        public Team Opponent1 { get; set; }
        public Team Opponent2 { get; set; }
        public int WinnerId { get; set; }
        public League League { get; set; }

        public DateTime BeginAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<Game> Games { get; set; }
        public Live Live { get; set; }
        public string EmbeddedUrl { get; set; }
        public MatchType MatchType { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string OfficialStreamUrl { get; set; }
        public bool Rescheduled { get; set; }
        public Result Result { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Series Series { get; set; }
        public MatchStatusType Status { get; set; }
        public Tournament Tournament { get; set; }
        public Videogame Videogame { get; set; }
    }
}
