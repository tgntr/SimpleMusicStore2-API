using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activities;

        public ActivityController(IActivityService activities)
            :base()
        {
            _activities = activities;
        }
        public IEnumerable<RecordDetails> Wishlist()
        {
            return _activities.Wishlist();
        }

        public IEnumerable<ArtistDetails> FollowedArtists()
        {
            return _activities.FollowedArtists();
        }

        public IEnumerable<LabelDetails> FollowedLabels()
        {
            return _activities.FollowedLabels();
        }

        public IEnumerable<OrderDetails> Orders()
        {
            return _activities.Orders();
        }
    }
}