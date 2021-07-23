using System.Collections.Generic;

namespace SimpleMusicStore.Models.View
{
    public class Browse
    {
        public IEnumerable<string> AvailableGenres { get; set; }
        public IEnumerable<string> AvailableFormats { get; set; }
        public IEnumerable<string> AvailableSortTypes { get; set; }
    }
}
