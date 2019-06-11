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
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ICurrentUser _currentUser;

        public ActivityService(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public IEnumerable<RecordDetails> Wishlist()
        {
            //Reverse will order them by activity date, so the newest activities will be on top
            return _currentUser.Wishlist().Reverse();
        }

        public IEnumerable<ArtistDetails> FollowedArtists()
        {
            return _currentUser.FollowedArtists().Reverse();
        }

        public IEnumerable<LabelDetails> FollowedLabels()
        {
            return _currentUser.FollowedLabels().Reverse();
        }

        public IEnumerable<OrderDetails> Orders()
        {
            return _currentUser.Orders<OrderDetails>().Reverse();
        }
    }
}
