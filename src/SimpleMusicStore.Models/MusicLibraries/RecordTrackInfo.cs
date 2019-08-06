using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SimpleMusicStore.ModelValidations;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordTrackInfo
    {
        public string Duration { get; set; }
        [Required]
        public string Title { get; set; }
    }
}