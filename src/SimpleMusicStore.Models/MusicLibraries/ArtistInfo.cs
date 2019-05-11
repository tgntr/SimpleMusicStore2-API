using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class ArtistInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ImageInfo[] Images { get; set; }

        public string Profile { get; set; }
    }
}
