using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordTrackInfo
    {
        public string Duration { get; set; }
        [Required]
        public string Title { get; set; }
        [Url]
        public string Preview { get; set; }
    }
}