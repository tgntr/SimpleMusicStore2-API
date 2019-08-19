using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMusicStore.Services
{
    public class CurrentUserActivities : ICurrentUserActivities
    {
        private readonly UserDetails _currentUser;
        private readonly IMapper _mapper;

        public CurrentUserActivities(IUserRepository users, IClaimAccessor currentUserClaims, IMapper mapper)
        {
            IsAuthenticated = currentUserClaims.IsAuthenticated;
            _mapper = mapper;
            if (IsAuthenticated)
            {
                //todo better way than getawaiter.getresult
                _currentUser = users.Find(currentUserClaims.Id).GetAwaiter().GetResult();
                Id = currentUserClaims.Id;
            }
        }

        public string Id { get; }
        public bool IsAuthenticated { get; }

        public IEnumerable<WishDetails> Wishlist => 
            _currentUser.Wishlist;

        public IEnumerable<ArtistFollowDetails> FollowedArtists => 
            _currentUser.FollowedArtists;

        public IEnumerable<LabelFollowDetails> FollowedLabels => 
            _currentUser.FollowedLabels;

        public IEnumerable<OrderView> Orders => 
            _currentUser.Orders;

        public IEnumerable<WishDetails> WishlistOrdered() => 
            Wishlist.OrderByDescending(o => o.Date);

        public IEnumerable<ArtistFollowDetails> FollowedArtistsOrdered() => 
            FollowedArtists.OrderByDescending(fa => fa.Date);

        public IEnumerable<LabelFollowDetails> FollowedLabelsOrdered() => 
            FollowedLabels.OrderByDescending(fl => fl.Date);

        public IEnumerable<OrderDetails> OrdersOrdered() => 
            Orders.OrderByDescending(o => o.Date).Select(_mapper.Map<OrderDetails>);

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
    }
}
