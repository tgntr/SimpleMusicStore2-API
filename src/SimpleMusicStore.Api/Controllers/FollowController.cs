using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;

namespace SimpleMusicStore.Api.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly IFollowService _follows;

        public FollowController(IFollowService follows)
            : base()
        {
            _follows = follows;
        }

        public async Task AddToWishlist(int id)
        {
            await _follows.AddToWishlist(id);
        }

        public async Task RemoveFromWishlist(int id)
        {
            await _follows.RemoveFromWishlist(id);
        }

        public async Task FollowArtist(int id)
        {
            await _follows.FollowArtist(id);
        }

        public async Task UnfollowArtist(int id)
        {
            await _follows.UnfollowArtist(id);
        }

        public async Task FollowLabel(int id)
        {
            await _follows.FollowLabel(id);
        }

        public async Task UnfollowLabel(int id)
        {
            await _follows.UnfollowLabel(id);
        }
    }
}