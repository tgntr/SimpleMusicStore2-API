using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ICurrentUserActivities
    {
        IEnumerable<RecordDetails> Wishlist { get; }
        IEnumerable<ArtistDetails> FollowedArtists { get; }
        IEnumerable<LabelDetails> FollowedLabels { get; }
        IEnumerable<OrderView> Orders { get; }
        IEnumerable<OrderDetails> OrdersOrdered();
        IEnumerable<RecordDetails> WishlistOrdered();
        IEnumerable<ArtistDetails> FollowedArtistsOrdered();
        IEnumerable<LabelDetails> FollowedLabelsOrdered();
        bool IsRecordInWishlist(int recordId);
        bool IsArtistFollowed(int artistId);
        bool IsLabelFollowed(int labelId);
        bool IsAuthenticated { get; }
        string Id { get; }
    }
}
