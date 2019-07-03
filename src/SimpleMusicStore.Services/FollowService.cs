using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _db;
        private readonly IServiceValidator _validator;
        private readonly IClaimAccessor _currentUser;

        public FollowService(IUnitOfWork db, IServiceValidator validator, IClaimAccessor currentUser)
        {
            _db = db;
            _validator = validator;
            _currentUser = currentUser;
        }

        public async Task AddToWishlist(int recordId)
        {
            await _validator.RecordExists(recordId);
            await _validator.RecordIsNotInWishlist(recordId);
            await AddRecordToWishlist(recordId);
        }

        public async Task FollowArtist(int artistId)
        {
            await _validator.ArtistExists(artistId);
            await _validator.ArtistIsNotFollowed(artistId);
            await AddArtistFollow(artistId);
        }

        public async Task FollowLabel(int labelId)
        {
            await _validator.LabelExists(labelId);
            await _validator.LabelIsNotFollowed(labelId);
            await AddLabelFollow(labelId);
        }

        public async Task RemoveFromWishlist(int recordId)
        {
            await _db.Wishes.Delete(recordId, _currentUser.Id);
            await _db.SaveChanges();
        }

        public async Task UnfollowArtist(int artistId)
        {
            await _db.ArtistFollows.Delete(artistId, _currentUser.Id);
            await _db.SaveChanges();
        }

        public async Task UnfollowLabel(int labelId)
        {
            await _db.LabelFollows.Delete(labelId, _currentUser.Id);
            await _db.SaveChanges();
        }

        private async Task AddRecordToWishlist(int recordId)
        {
            await _db.Wishes.Add(recordId, _currentUser.Id);
            await _db.SaveChanges();
        }

        private async Task AddArtistFollow(int artistId)
        {
            await _db.ArtistFollows.Add(artistId, _currentUser.Id);
            await _db.SaveChanges();
        }

        private async Task AddLabelFollow(int labelId)
        {
            await _db.LabelFollows.Add(labelId, _currentUser.Id);
            await _db.SaveChanges();
        }
    }
}
