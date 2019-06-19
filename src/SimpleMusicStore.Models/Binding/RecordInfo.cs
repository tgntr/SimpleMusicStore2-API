using SimpleMusicStore.Constants;
using SimpleMusicStore.Models.MusicLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Models.Binding
{
    public class RecordInfo
    {
        public RecordInfo()
        {
            Videos = new List<RecordVideoInfo>();
            Images = new List<ImageInfo>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Range(1, 100.00)]
        public decimal Price { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public ICollection<RecordVideoInfo> Videos { get; set; }
        [MinLength(1)]
        public ICollection<RecordLabelInfo> Labels { get; set; }
        [MinLength(1)]
        public ICollection<RecordArtistInfo> Artists { get; set; }
        [MinLength(1)]
        public ICollection<ImageInfo> Images { get; set; }
        [MinLength(1)]
        public ICollection<string> Genres { get; set; }
        [MinLength(1)]
        public ICollection<RecordTrackInfo> Tracklist { get; set; }
        [MinLength(1)]
        public ICollection<RecordFormatInfo> Formats { get; set; }

        public string Format() => Formats.First().Name;
        public string Image()
        {
            if (Images is null || !Images.Any())
                return DiscogsConstants.DEFAULT_IMAGE;
            else
                return Images.First().Uri;
        }
        public int ArtistId() => Artists.First().Id;
        public int LabelId() => Labels.First().Id;
        public string Genre() => Genres.First();
    }
}
