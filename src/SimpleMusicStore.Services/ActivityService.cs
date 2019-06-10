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
        private readonly SimpleUser _currentUser;
        private readonly IMapper _mapper;
        public ActivityService(UserManager<SimpleUser> users, IClaimAccessor currentUser, IMapper mapper)
        {
            //todo too ugly
            _currentUser = users.FindByIdAsync(currentUser.Id).GetAwaiter().GetResult();
            _mapper = mapper;
        }

        public IEnumerable<RecordDetails> Wishlist()
        {
            return _currentUser.Wishlist
                .OrderByDescending(w => w.Date)
                .Select(w => w.Record)
                .Select(_mapper.Map<RecordDetails>);
        }

        public IEnumerable<ArtistDetails> FollowedArtists()
        {
            return _currentUser.FollowedArtists
                .OrderByDescending(fa=>fa.Date)
                .Select(fa => fa.Artist)
                .Select(_mapper.Map<ArtistDetails>);
        }

        public IEnumerable<LabelDetails> FollowedLabels()
        {
            return _currentUser.FollowedLabels
                .OrderByDescending(fl=>fl.Date)
                .Select(fl => fl.Label)
                .Select(_mapper.Map<LabelDetails>);
        }

        public IEnumerable<OrderDetails> Orders()
        {
            return _currentUser.Orders
                .OrderByDescending(o => o.Date)
                .Select(_mapper.Map<OrderDetails>);
        }
    }
}
