using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.View
{
    public class SearchResult
    {
        public IEnumerable<RecordDetails> Records { get; set; }
        public IEnumerable<ArtistDetails> Artists { get; set; }
        public IEnumerable<LabelDetails> Labels { get; set; }
    }
}
