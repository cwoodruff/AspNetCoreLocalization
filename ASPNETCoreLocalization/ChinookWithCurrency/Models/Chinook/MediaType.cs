﻿using System.Collections.Generic;

namespace ChinookWithCurrency.Models.Chinook
{
    public sealed class MediaType
    {
        public MediaType()
        {
            Track = new HashSet<Track>();
        }

        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Track> Track { get; set; }
    }
}
