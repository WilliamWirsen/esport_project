using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class Match
    {
        public int ID { get; set; }
        public bool draw { get; set; }
        public bool forfeit { get; set; }
        public string matchType { get; set; }
        public string name { get; set; }
        public int numberOfGames { get; set; }
        public Team opponent1 { get; set; }
        public Team opponent2 { get; set; }
        public int winnerId { get; set; }
        public League league { get; set; }
    }
}
