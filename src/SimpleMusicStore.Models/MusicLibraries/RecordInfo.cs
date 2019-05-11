using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Models.MusicLibraries
{
    public class RecordInfo
    {
        public RecordVideoInfo[] Videos { get; set; }

        public RecordLabelInfo[] Labels { get; set; }

        public RecordArtistInfo[] Artists { get; set; }

        public ImageInfo[] Images { get; set; }

        public int Id { get; set; }

        public string[] Genres { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public RecordTrackInfo[] Tracklist { get; set; }

        public List<RecordFormatDto> Formats { get; set; }



    }
}
