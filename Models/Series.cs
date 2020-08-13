using esport.Models.Enums;
using esport.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esport.Models
{
    public class Series : IBaseInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prizepool { get; set; }
        public string Season { get; set; }
        public int? Year { get; set; }
        public DateTime BeginDate { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public string FullName { get; set; }
        public int LeagueId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Tier { get; set; }
        public Winner Winner { get; set; }
        public string Slug { get; set; }
    }
}
