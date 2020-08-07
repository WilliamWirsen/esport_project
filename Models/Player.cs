using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class Player
    {
        public string BirthYear { get; set; }
        public DateTime? Birthday { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Role { get; set; }
        public string Slug { get; set; }
        public string HomeTown { get; set; }
        public int Id { get; set; }
        public string ImgUrl { get; set; }

    }
}
