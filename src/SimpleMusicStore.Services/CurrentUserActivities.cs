﻿using AutoMapper;
using PagedList;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
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

        public int Id { get; }
        public bool IsAuthenticated { get; }

        public IEnumerable<WishDetails> Wishlist =>
            _currentUser.Wishlist;

        public IEnumerable<ArtistFollowDetails> FollowedArtists =>
            _currentUser.FollowedArtists;

        public IEnumerable<LabelFollowDetails> FollowedLabels =>
            _currentUser.FollowedLabels;

        public IEnumerable<OrderView> Orders =>
            _currentUser.Orders;

        public IEnumerable<WishDetails> WishlistOrdered(int page) =>
            Wishlist.OrderByDescending(o => o.Date).ToPagedList(page, CommonConstants.PAGE_SIZE);

        public IEnumerable<ArtistFollowDetails> FollowedArtistsOrdered(int page) =>
            FollowedArtists.OrderByDescending(fa => fa.Date).ToPagedList(page, CommonConstants.PAGE_SIZE);

        public IEnumerable<LabelFollowDetails> FollowedLabelsOrdered(int page) =>
            FollowedLabels.OrderByDescending(fl => fl.Date).ToPagedList(page, CommonConstants.PAGE_SIZE);

        public IEnumerable<OrderDetails> OrdersOrdered(int page) =>
            Orders.OrderByDescending(o => o.Date).ToPagedList(page, CommonConstants.PAGE_SIZE).Select(_mapper.Map<OrderDetails>);

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
