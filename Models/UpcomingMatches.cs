namespace esport.Models
{
    public class UpcomingMatches
    {
        public int ID { get; set; }
        public string StartDate { get; set; }
        public bool Draw { get; set; }
        public string MatchType { get; set; }
        public string Name { get; set; }
        public int NumberOfGames { get; set; }
        public string Tournament { get; set; }
        public League League { get; set; }
        public Team OpponentOne { get; set; }
        public Team OpponentTwo { get; set; }
        public Series Series { get; set; }
    }
}
