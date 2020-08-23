using esport.Models.Enums;

namespace esport.Models
{
    public class Videogame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Version { get; set; }
        public GameType Type { get; set; }
        public string ImgUrl { get; set; }
    }
}