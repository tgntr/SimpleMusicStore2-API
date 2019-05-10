using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscogsUtilities.Models
{
    public class LabelDto
    {
        public int Id { get; set; }

        public string Profile { get; set; }

        public string Name { get; set; }

        public ImageDto[] Images { get; set; }
    }
}
