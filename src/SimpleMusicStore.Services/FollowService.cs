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
        private readonly IWishRepository _wishes;
        private readonly IArtistFollowRepository _artistFollows;
        private readonly ILabelFollowRepository _labelFollows;
        private readonly IClaimAccessor _currentUser;
        private readonly IServiceValidator _validator;

        public FollowService(IWishRepository wishes,
            IArtistFollowRepository artistFollows,
            ILabelFollowRepository labelFollows,
            IServiceValidator validator,
            IClaimAccessor currentUser)
        {
            _wishes = wishes;
            _artistFollows = artistFollows;
            _labelFollows = labelFollows;
            _currentUser = currentUser;
            _validator = validator;
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
            await _validator.RecordIsInWishlist(recordId);
            await _wishes.Delete(recordId, _currentUser.Id);
            await _wishes.SaveChanges();
        }

        public async Task UnfollowArtist(int artistId)
        {
            await _validator.ArtistIsFollowed(artistId);
            await _artistFollows.Delete(artistId, _currentUser.Id);
            await _artistFollows.SaveChanges();
        }

        public async Task UnfollowLabel(int labelId)
        {
            await _validator.LabelIsFollowed(labelId);
            await _labelFollows.Delete(labelId, _currentUser.Id);
            await _labelFollows.SaveChanges();
        }

        private async Task AddRecordToWishlist(int recordId)
        {
            await _wishes.Add(new Wish { RecordId = recordId, UserId = _currentUser.Id });
            await _wishes.SaveChanges();
        }

        private async Task AddArtistFollow(int artistId)
        {
            await _artistFollows.Add(new ArtistFollow() { ArtistId = artistId, UserId = _currentUser.Id });
            await _artistFollows.SaveChanges();
        }

        private async Task AddLabelFollow(int labelId)
        {
            await _labelFollows.Add(new LabelFollow() { LabelId = labelId, UserId = _currentUser.Id });
            await _labelFollows.SaveChanges();
        }
    }
}
