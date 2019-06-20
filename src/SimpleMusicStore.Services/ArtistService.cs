using AutoMapper;
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
        private readonly IArtistRepository _artists;
        private readonly IMapper _mapper;
        private readonly MusicSource _discogs;
        private readonly IServiceValidator _validator;
        private readonly ICurrentUserActivities _currentUser;

        public ArtistService(IArtistRepository artists,
            IMapper mapper,
            MusicSource discogs,
            IServiceValidator validator,
            ICurrentUserActivities currentUser)
        {
            _artists = artists;
            _mapper = mapper;
            _discogs = discogs;
            _validator = validator;
            _currentUser = currentUser;
        }

        public async Task Add(int discogsId)
        {
            if (!await _artists.Exists(discogsId))
                await AddArtist(discogsId);
        }

        public async Task<ArtistView> Find(int id)
        {
            await _validator.ArtistExists(id);
            return await GenerateArtistView(id);
        }

        private async Task<ArtistView> GenerateArtistView(int id)
        {
            var artist = await _artists.Find(id);
            artist.IsFollowed = _currentUser.IsArtistFollowed(id);
            artist.Records = artist.Records.OrderByDescending(r => r.DateAdded);
            return artist;
        }

        private async Task AddArtist(int discogsId)
        {
            var artistInfo = await _discogs.Artist(discogsId);
            var artist = _mapper.Map<Artist>(artistInfo);
            await _artists.Add(artist);
        }
    }
}
