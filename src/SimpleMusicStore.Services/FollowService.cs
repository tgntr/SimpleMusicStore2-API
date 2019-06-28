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

        public FollowService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task AddToWishlist(int recordId)
        {
            await _db.Validator.RecordExists(recordId);
            await _db.Validator.RecordIsNotInWishlist(recordId);
            await AddRecordToWishlist(recordId);
        }

        public async Task FollowArtist(int artistId)
        {
            await _db.Validator.ArtistExists(artistId);
            await _db.Validator.ArtistIsNotFollowed(artistId);
            await AddArtistFollow(artistId);
        }

        public async Task FollowLabel(int labelId)
        {
            await _db.Validator.LabelExists(labelId);
            await _db.Validator.LabelIsNotFollowed(labelId);
            await AddLabelFollow(labelId);
        }

        public async Task RemoveFromWishlist(int recordId)
        {
            await _db.Wishes.Delete(recordId, _db.CurrentUser.Id);
            await _db.SaveChanges();
        }

        public async Task UnfollowArtist(int artistId)
        {
            await _db.ArtistFollows.Delete(artistId, _db.CurrentUser.Id);
            await _db.SaveChanges();
        }

        public async Task UnfollowLabel(int labelId)
        {
            await _db.LabelFollows.Delete(labelId, _db.CurrentUser.Id);
            await _db.SaveChanges();
        }

        private async Task AddRecordToWishlist(int recordId)
        {
            await _db.Wishes.Add(recordId, _db.CurrentUser.Id);
            await _db.SaveChanges();
        }

        private async Task AddArtistFollow(int artistId)
        {
            await _db.ArtistFollows.Add(artistId, _db.CurrentUser.Id);
            await _db.SaveChanges();
        }

        private async Task AddLabelFollow(int labelId)
        {
            await _db.LabelFollows.Add(labelId, _db.CurrentUser.Id);
            await _db.SaveChanges();
        }
    }
}
