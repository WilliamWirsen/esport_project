using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string acronym { get; set; }
        public string imgUrl { get; set; }
        public string name { get; set; }
        public List<Player> Players { get; set; }
        public string CurrentVideogame { get; set; }
    }
}
