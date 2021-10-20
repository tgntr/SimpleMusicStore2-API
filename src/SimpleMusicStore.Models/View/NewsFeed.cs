using System.Collections.Generic;

namespace SimpleMusicStore.Models.View
{
    public class NewsFeed
    {
        public IEnumerable<RecordDetails> Recommended { get; set; }
        public IEnumerable<RecordDetails> Newest { get; set; }
        public IEnumerable<RecordDetails> MostPopular { get; set; }
    }
}
