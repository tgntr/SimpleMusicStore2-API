using System;

namespace SimpleMusicStore.Models.View
{
    public class WishDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        //public LabelDetails Label { get; set; }
        //public ArtistDetails Artist { get; set; }
        public DateTime Date { get; set; }
    }
}
