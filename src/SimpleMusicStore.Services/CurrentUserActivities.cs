using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Services
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
                _currentUser = users.FindByIdAsync(currentUserClaims.Id).GetAwaiter().GetResult();
            
            
        }

        public bool IsAuthenticated { get; }

        public IEnumerable<RecordDetails> Wishlist =>
            MapWishesToDto(_currentUser.Wishlist);

        public IEnumerable<ArtistDetails> FollowedArtists =>
            MapArtistFollowsToDto(_currentUser.FollowedArtists);

        public IEnumerable<LabelDetails> FollowedLabels =>
            MapLabelFollowsToDto(_currentUser.FollowedLabels);

        public IEnumerable<RecordDetails> WishlistOrdered =>
            MapWishesToDto(_currentUser.Wishlist.OrderByDescending(w=>w.Date));

        public IEnumerable<ArtistDetails> FollowedArtistsOrdered =>
            MapArtistFollowsToDto(_currentUser.FollowedArtists.OrderByDescending(fa=>fa.Date));

        public IEnumerable<LabelDetails> FollowedLabelsOrdered =>
            MapLabelFollowsToDto(_currentUser.FollowedLabels.OrderByDescending(fl=>fl.Date));

        public IEnumerable<OrderView> Orders =>
            _currentUser.Orders
                .Select(_mapper.Map<OrderView>);

        public IEnumerable<OrderDetails> OrdersOrdered =>
            _currentUser.Orders
                .OrderByDescending(o=>o.Date)
                .Select(_mapper.Map<OrderDetails>);

        public bool IsRecordInWishlist(int recordId)
        {
            if (!IsAuthenticated)
                return false;
            else
                return _currentUser.Wishlist.Any(w => w.RecordId == recordId);
        }

        public bool IsArtistFollowed(int artistId)
        {
            if (!IsAuthenticated)
                return false;
            else
                return _currentUser.FollowedArtists.Any(af => af.ArtistId == artistId);
        }

        public bool IsLabelFollowed(int labelId)
        {
            if (!IsAuthenticated)
                return false;
            else 
                return _currentUser.FollowedLabels.Any(lf => lf.LabelId == labelId);
        }
            

        private IEnumerable<RecordDetails> MapWishesToDto(IEnumerable<Wish> wishes) => 
            wishes.Select(_mapper.Map<RecordDetails>);
        private IEnumerable<LabelDetails> MapLabelFollowsToDto(IEnumerable<LabelFollow> labelFollows) => 
            labelFollows.Select(_mapper.Map<LabelDetails>);
        private IEnumerable<ArtistDetails> MapArtistFollowsToDto(IEnumerable<ArtistFollow> artistFollows) => 
            artistFollows.Select(_mapper.Map<ArtistDetails>);
    }
}
