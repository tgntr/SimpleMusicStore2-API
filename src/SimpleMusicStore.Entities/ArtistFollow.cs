using SimpleMusicStore.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class ArtistFollow : UserActivity
    {
        public ArtistFollow()
            : base()
        {
        }

        public ArtistFollow(int artistId, int userId)
            : base()
        {
            ArtistId = artistId;
            UserId = userId;
        }

        [Required]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

    }
}
