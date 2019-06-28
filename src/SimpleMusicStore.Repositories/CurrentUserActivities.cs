using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMusicStore.Repositories
{
    public class CurrentUserActivities : ICurrentUserActivities
    {
        private readonly User _currentUser;
        private readonly IMapper _mapper;

        public CurrentUserActivities(UserManager<User> users, IClaimAccessor currentUserClaims, IMapper mapper)
        {
            IsAuthenticated = currentUserClaims.IsAuthenticated;
            _mapper = mapper;
            if (IsAuthenticated)
            {
                _currentUser = users.FindByIdAsync(currentUserClaims.Id).GetAwaiter().GetResult();
                Id = currentUserClaims.Id;
            }
        }

        public string Id { get; }
        public bool IsAuthenticated { get; }

        public IEnumerable<RecordDetails> Wishlist =>
            WishesAsDto(_currentUser.Wishlist);

        public IEnumerable<ArtistDetails> FollowedArtists =>
            ArtistFollowsAsDto(_currentUser.FollowedArtists);

        public IEnumerable<LabelDetails> FollowedLabels =>
            LabelFollowsAsDto(_currentUser.FollowedLabels);

        public IEnumerable<OrderView> Orders =>
            _currentUser.Orders
                .Select(_mapper.Map<OrderView>);

        public IEnumerable<RecordDetails> WishlistOrdered() =>
            WishesAsDto(_currentUser.Wishlist.OrderByDescending(w=>w.Date));

        public IEnumerable<ArtistDetails> FollowedArtistsOrdered() =>
            ArtistFollowsAsDto(_currentUser.FollowedArtists.OrderByDescending(fa=>fa.Date));

        public IEnumerable<LabelDetails> FollowedLabelsOrdered() =>
            LabelFollowsAsDto(_currentUser.FollowedLabels.OrderByDescending(fl=>fl.Date));

        public IEnumerable<OrderDetails> OrdersOrdered() =>
            _currentUser.Orders
                .OrderByDescending(o=>o.Date)
                .Select(_mapper.Map<OrderDetails>);

        public bool IsRecordInWishlist(int recordId)
        {
            if (!IsAuthenticated)
                return false;
            else
                return Wishlist.Any(w => w.Id == recordId);
        }
        
        public bool IsArtistFollowed(int artistId)
        {
            if (!IsAuthenticated)
                return false;
            else
                return FollowedArtists.Any(af => af.Id == artistId);
        }

        public bool IsLabelFollowed(int labelId)
        {
            if (!IsAuthenticated)
                return false;
            else 
                return FollowedLabels.Any(lf => lf.Id == labelId);
        }
            

        private IEnumerable<RecordDetails> WishesAsDto(IEnumerable<Wish> wishes) => 
            wishes.Select(_mapper.Map<RecordDetails>);
        private IEnumerable<LabelDetails> LabelFollowsAsDto(IEnumerable<LabelFollow> labelFollows) => 
            labelFollows.Select(_mapper.Map<LabelDetails>);
        private IEnumerable<ArtistDetails> ArtistFollowsAsDto(IEnumerable<ArtistFollow> artistFollows) => 
            artistFollows.Select(_mapper.Map<ArtistDetails>);
    }
}
