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
        private readonly IServiceValidations _validator;

        public ArtistService(
            IArtistRepository artists, 
            IMapper mapper, 
            MusicSource discogs,
            IServiceValidations validator)
        {
            _artists = artists;
            _mapper = mapper;
            _discogs = discogs;
            _validator = validator;
        }

        public async Task Add(int discogsId)
        {
            if (!await _artists.Exists(discogsId))
                await AddArtist(discogsId);
        }

        private async Task AddArtist(int discogsId)
        {
            var artistInfo = await _discogs.Artist(discogsId);
            var artist = _mapper.Map<Artist>(artistInfo);
            await _artists.Add(artist);
        }

        
    }
}
