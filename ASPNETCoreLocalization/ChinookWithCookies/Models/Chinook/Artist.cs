using System.Collections.Generic;

namespace ChinookWithCookies.Models.Chinook
{
    public sealed class Artist
    {
        public Artist()
        {
            Album = new HashSet<Album>();
        }

        public int ArtistId { get; set; }
        public string Name { get; set; }

        public ICollection<Album> Album { get; set; }
    }
}
