using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface ICurrentUserActivities
    {
        IEnumerable<WishDetails> Wishlist { get; }
        IEnumerable<ArtistFollowDetails> FollowedArtists { get; }
        IEnumerable<LabelFollowDetails> FollowedLabels { get; }
        IEnumerable<OrderView> Orders { get; }
        IEnumerable<OrderDetails> OrdersOrdered();
        IEnumerable<WishDetails> WishlistOrdered();
        IEnumerable<ArtistFollowDetails> FollowedArtistsOrdered();
        IEnumerable<LabelFollowDetails> FollowedLabelsOrdered();
        bool IsRecordInWishlist(int recordId);
        bool IsArtistFollowed(int artistId);
        bool IsLabelFollowed(int labelId);
        bool IsAuthenticated { get; }
        int Id { get; }
    }
}
