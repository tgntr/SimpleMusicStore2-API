using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IActivityService
    {
        IEnumerable<RecordDetails> Wishlist();
        IEnumerable<ArtistDetails> FollowedArtists();
        IEnumerable<LabelDetails> FollowedLabels();
        IEnumerable<OrderDetails> Orders();
    }
}
