﻿using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _db;
        private readonly ICurrentUserActivities _currentUser;

        public ArtistService(IUnitOfWork db, ICurrentUserActivities currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<ArtistView> Find(int id)
        {
            return await GenerateArtistView(id);
        }

        private async Task<ArtistView> GenerateArtistView(int id)
        {
            var artist = await _db.Artists.Find(id);
            artist.IsFollowed = _currentUser.IsArtistFollowed(id);
            return artist;
        }
    }
}
