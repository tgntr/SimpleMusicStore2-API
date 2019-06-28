using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly ICurrentUserActivities _currentUser;

        public ActivityController(ICurrentUserActivities currentUser)
            :base()
        {
            _currentUser = currentUser;
        }
        public IEnumerable<WishDetails> Wishlist()
        {
            return _currentUser.WishlistOrdered();
        }

        public IEnumerable<ArtistFollowDetails> FollowedArtists()
        {
            return _currentUser.FollowedArtistsOrdered();
        }

        public IEnumerable<LabelFollowDetails> FollowedLabels()
        {
            return _currentUser.FollowedLabelsOrdered();
        }

        public IEnumerable<OrderView> Orders()
        {
            return _currentUser.OrdersOrdered();
        }
    }
}