using Microsoft.AspNetCore.Http;
using SimpleMusicStore.ModelValidations;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordTrackInfo
    {
        public string Duration { get; set; }
        [Required]
        public string Title { get; set; }
        //[Required, NonEmptyMp3File]
        public IFormFile Preview { get; set; }
    }
}