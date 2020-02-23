using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class League
    {
        public League()
        {
            Matches = new List<UpcomingMatches>();
        }
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public Game Videogame { get; set; }
        public List<UpcomingMatches> Matches { get; set; }

    }
}
