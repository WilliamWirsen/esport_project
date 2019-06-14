using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class Series
    {
        public int id { get; set; }
        public string name { get; set; }
        public string prizepool { get; set; }
        public string season { get; set; }
        public string status { get; set; }
        public int year { get; set; }
    }
}
