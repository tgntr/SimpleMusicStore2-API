using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordInfo
    {
        public RecordInfo()
        {
            Videos = new List<RecordVideoInfo>();
            Images = new List<ImageInfo>();
        }
        //TODO check if deserializes with private properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public ICollection<RecordVideoInfo> Videos { get; set; }
        public ICollection<RecordLabelInfo> Labels { get; set; }
        public ICollection<RecordArtistInfo> Artists { get; set; }
        public ICollection<ImageInfo> Images { get; set; }
        public ICollection<string> Genres { get; set; }
        public ICollection<RecordTrackInfo> Tracklist { get; set; }
        public ICollection<RecordFormatInfo> Formats { get; set; }

        public string Format => Formats.First().Name;
        public string Image => Images.First().Uri;
        public int ArtistId => Artists.First().Id;
        public int LabelId => Labels.First().Id;





    }
}
