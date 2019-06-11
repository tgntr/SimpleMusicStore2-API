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
    public class CurrentUser : ICurrentUser
    {
        private readonly SimpleUser _currentUser;
        private readonly IMapper _mapper;

        public CurrentUser(UserManager<SimpleUser> users, IClaimAccessor currentUserClaims, IMapper mapper)
        {
            IsAuthenticated = currentUserClaims.IsAuthenticated;
            _mapper = mapper;
            if (IsAuthenticated)
                _currentUser = users.FindByIdAsync(currentUserClaims.Id).GetAwaiter().GetResult();
            
            
        }

        public IEnumerable<RecordDetails> Wishlist() =>
            _currentUser.Wishlist
                .Select(w => w.Record)
                .Select(_mapper.Map<RecordDetails>);

        public IEnumerable<ArtistDetails> FollowedArtists() =>
            _currentUser.FollowedArtists
                .Select(fa => fa.Artist)
                .Select(_mapper.Map<ArtistDetails>);

        public IEnumerable<LabelDetails> FollowedLabels() =>
            _currentUser.FollowedLabels
                .Select(fl => fl.Label)
                .Select(_mapper.Map<LabelDetails>);

        public IEnumerable<T> Orders<T>() =>
            _currentUser.Orders
                .Select(_mapper.Map<T>);

        public bool IsAuthenticated { get; }
    }
}
