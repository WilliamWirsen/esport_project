using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class League
    {
        public int ID { get; set; }
        public string imgUrl { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string videogame { get; set; }
    }
}
