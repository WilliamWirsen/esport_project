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
            Matches = new List<UpcomingMatch>();
        }
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public Game Videogame { get; set; }
        public List<UpcomingMatch> Matches { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
