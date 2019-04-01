using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public partial class Playlists
    {
        public Playlists()
        {
            PlaylistTrack = new HashSet<PlaylistTrack>();
        }

        public long PlaylistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTrack { get; set; }
    }
}
