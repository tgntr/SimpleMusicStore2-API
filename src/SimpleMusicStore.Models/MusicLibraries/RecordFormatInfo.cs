using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordFormatInfo
    {
        [Required]
        public string Name { get; set; }
    }
}
