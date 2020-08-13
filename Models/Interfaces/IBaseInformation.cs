using System;

namespace esport.Models.Interfaces
{
    public interface IBaseInformation
    {
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}