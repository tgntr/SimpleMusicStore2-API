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
        IEnumerable<OrderDetails> OrdersOrdered(int page);
        IEnumerable<WishDetails> WishlistOrdered(int page);
        IEnumerable<ArtistFollowDetails> FollowedArtistsOrdered(int page);
        IEnumerable<LabelFollowDetails> FollowedLabelsOrdered(int page);
        bool IsRecordInWishlist(int recordId);
        bool IsArtistFollowed(int artistId);
        bool IsLabelFollowed(int labelId);
        bool IsAuthenticated { get; }
        int Id { get; }
    }
}
