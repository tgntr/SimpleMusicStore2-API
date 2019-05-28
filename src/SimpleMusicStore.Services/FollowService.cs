using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class FollowService : IFollowService
    {
        private readonly IWishRepository _wishes;
        private readonly IArtistFollowRepository _artistFollows;
        private readonly ILabelFollowRepository _labelFollows;
        private readonly string _currentUserId;
        private readonly IServiceValidations _validator;

        public FollowService(
			IWishRepository wishes, 
			IArtistFollowRepository artistFollows, 
			ILabelFollowRepository labelFollows, 
			IHttpContextAccessor httpContext,
            IServiceValidations validator)
        {
            _wishes = wishes;
            _artistFollows = artistFollows;
            _labelFollows = labelFollows;
            _currentUserId = httpContext.HttpContext.User.FindFirstValue("id");
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
			await _wishes.Delete(recordId, _currentUserId);
		}

		public async Task UnfollowArtist(int artistId)
        {
            await _validator.ArtistIsFollowed(artistId);
			await _artistFollows.Delete(artistId, _currentUserId);
        }

		public async Task UnfollowLabel(int labelId)
        {
            await _validator.LabelIsFollowed(labelId);
			await _labelFollows.Delete(labelId, _currentUserId);
        }

		private async Task AddRecordToWishlist(int recordId)
        {
            await _wishes.Add(new Wish { RecordId = recordId, UserId = _currentUserId });
            await _wishes.SaveChanges();
        }

		private async Task AddArtistFollow(int artistId)
		{
			await _artistFollows.Add(new ArtistFollow() { ArtistId = artistId, UserId = _currentUserId });
			await _artistFollows.SaveChanges();
		}

		private async Task AddLabelFollow(int labelId)
		{
			await _labelFollows.Add(new LabelFollow() { LabelId = labelId, UserId = _currentUserId });
			await _labelFollows.SaveChanges();
		}
	}
}
