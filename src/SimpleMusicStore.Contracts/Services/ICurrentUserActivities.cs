using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Services
{
    public interface ICurrentUserActivities
    {
        IEnumerable<RecordDetails> Wishlist { get; }
        IEnumerable<ArtistDetails> FollowedArtists { get; }
        IEnumerable<LabelDetails> FollowedLabels { get; }
        IEnumerable<RecordDetails> WishlistOrdered { get; }
        IEnumerable<ArtistDetails> FollowedArtistsOrdered { get; }
        IEnumerable<LabelDetails> FollowedLabelsOrdered { get; }
        IEnumerable<OrderView> Orders { get; }
        IEnumerable<OrderDetails> OrdersOrdered { get; }
        bool IsRecordInWishlist(int recordId);
        bool IsArtistFollowed(int artistId);
        bool IsLabelFollowed(int labelId);
        bool IsAuthenticated { get; }
    }
}
