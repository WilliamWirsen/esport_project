using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public string CurrentVideogame { get; set; }
        public string Location { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
