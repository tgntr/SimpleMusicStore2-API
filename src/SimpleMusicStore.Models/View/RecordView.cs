using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models.View
{
    public class RecordView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public ICollection<VideoDetails> Videos { get; set; }
        public ICollection<TrackDetails> Tracks { get; set; }
        public ArtistDetails Artist { get; set; }
        public LabelDetails Label { get; set; }
        public decimal Price { get; set; }
        public string Format { get; set; }
        public bool IsFollowed { get; set; }
    }
}
