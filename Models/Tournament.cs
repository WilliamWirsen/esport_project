using System;

namespace esport.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsLiveSupported { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Prizepool { get; set; }
        public int SeriesId { get; set; }
        public string Slug { get; set; }
        public int? WinnerId { get; set; }




    }
}