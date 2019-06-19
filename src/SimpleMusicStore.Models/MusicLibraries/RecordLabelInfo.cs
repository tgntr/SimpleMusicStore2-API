using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordLabelInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}