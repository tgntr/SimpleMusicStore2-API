using System.Collections.Generic;

namespace SimpleMusicStore.Models
{
    public class SubscriberDetails
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> FollowedArtists { get; set; }
        public IEnumerable<int> FollowedLabels { get; set; }
    }
}
