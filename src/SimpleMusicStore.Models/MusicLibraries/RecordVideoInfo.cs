using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordVideoInfo
    {
        [Required]
        public string Uri { get; set; }

    }
}