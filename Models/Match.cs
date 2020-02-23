namespace esport.Models
{
    public class Match
    {
        public int ID { get; set; }
        public bool Draw { get; set; }
        public bool Forfeit { get; set; }
        public string MatchType { get; set; }
        public string Name { get; set; }
        public int NumberOfGames { get; set; }
        public Team Opponent1 { get; set; }
        public Team Opponent2 { get; set; }
        public int WinnerId { get; set; }
        public League Weague { get; set; }
    }
}
