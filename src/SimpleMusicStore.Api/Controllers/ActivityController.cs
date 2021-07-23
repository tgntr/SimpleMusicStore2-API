﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly ICurrentUserActivities _currentUser;

        public ActivityController(ICurrentUserActivities currentUser)
            : base()
        {
            _currentUser = currentUser;
        }
        public IEnumerable<WishDetails> Wishlist(int page)
        {
            return _currentUser.WishlistOrdered(page);
        }

        public IEnumerable<ArtistFollowDetails> FollowedArtists(int page)
        {
            return _currentUser.FollowedArtistsOrdered(page);
        }

        public IEnumerable<LabelFollowDetails> FollowedLabels(int page)
        {
            return _currentUser.FollowedLabelsOrdered(page);
        }

        public IEnumerable<OrderDetails> Orders(int page)
        {
            return _currentUser.OrdersOrdered(page);
        }
    }
}