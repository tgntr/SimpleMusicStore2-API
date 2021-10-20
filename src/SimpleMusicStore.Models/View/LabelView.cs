using System.Collections.Generic;

namespace SimpleMusicStore.Models.View
{
    public class LabelView
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public IEnumerable<RecordDetails> Records { get; set; }
        public bool IsFollowed { get; set; }
    }
}
