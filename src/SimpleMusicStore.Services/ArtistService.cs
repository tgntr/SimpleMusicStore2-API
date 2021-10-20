using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
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
            var artist = await _db.Artists.Find(id);
            artist.IsFollowed = _currentUser.IsArtistFollowed(id);
            return artist;
        }
    }
}
