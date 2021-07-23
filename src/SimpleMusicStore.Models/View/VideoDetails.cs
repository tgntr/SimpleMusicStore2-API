using System.Linq;

namespace SimpleMusicStore.Models.View
{
    public class VideoDetails
    {
        public string Uri { get; set; }
        public string YoutubeId => Uri.Split("=").Last();
    }
}
