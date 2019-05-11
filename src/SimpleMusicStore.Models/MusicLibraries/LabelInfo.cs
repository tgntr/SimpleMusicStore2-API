using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class LabelInfo
    {
        public int Id { get; set; }

        public string Profile { get; set; }

        public string Name { get; set; }

        public ImageInfo[] Images { get; set; }
    }
}
