using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Models.View
{
    public class VideoDetails
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string YoutubeId => Url.Split("=").Last();
    }
}
