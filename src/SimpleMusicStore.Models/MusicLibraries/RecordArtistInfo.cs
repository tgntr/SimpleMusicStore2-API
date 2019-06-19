using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordArtistInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}