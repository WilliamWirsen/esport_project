using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class LiveGame
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public string matchType { get; set; }
        public string streamUrl { get; set; }
        public string linkUrl { get; set; }
        public bool draw { get; set; }
        public bool forfeit { get; set; }
        public Team opponentOne { get; set; }
        public Team opponentTwo { get; set; }
        public League league { get; set; }
        public int opponentOneResult { get; set; }
        public int opponentTwoResult { get; set; }
        public string season { get; set; }
        public int numberOfGames { get; set; }
        public string status { get; set; }
        public string Videogame { get; set; }
    }
}
