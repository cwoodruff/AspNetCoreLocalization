using System.Collections.Generic;

namespace ChinookWithCookies.Models.Chinook
{
    public sealed class Genre
    {
        public Genre()
        {
            Track = new HashSet<Track>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }

        public ICollection<Track> Track { get; set; }
    }
}
