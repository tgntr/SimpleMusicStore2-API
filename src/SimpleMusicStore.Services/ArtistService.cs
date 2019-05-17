using AutoMapper;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artists;
        private readonly IMapper _mapper;
        private readonly MusicSource _discogs;

        public ArtistService(IArtistRepository artists, IMapper mapper, MusicSource discogs)
        {
            _artists = artists;
            _mapper = mapper;
            _discogs = discogs;
        }

        public async Task Add(int discogsId)
        {
            if (await _artists.Exists(discogsId))
                return;

            var artistInfo = await _discogs.Artist(discogsId);
            var artist =  _mapper.Map<Artist>(artistInfo);
            await _artists.Add(artist);
        }
    }
}
