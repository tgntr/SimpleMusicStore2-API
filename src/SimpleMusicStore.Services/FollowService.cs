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
        private readonly IRecordService _records;

        public FollowService(IWishRepository wishes, IArtistFollowRepository artistFollows, ILabelFollowRepository labelFollows, IHttpContextAccessor httpContext, IRecordService records)
        {
            _wishes = wishes;
            _artistFollows = artistFollows;
            _labelFollows = labelFollows;
            _currentUserId = httpContext.HttpContext.User.FindFirstValue("id");
            _records = records;
        }

        public async Task AddToWishlist(int recordId)
        {
            await ValidateThatRecordExists(recordId);
            await ValidateThatRecordIsNotInWishlist(recordId);
            await AddRecordToWishlist(recordId);
        }

        public async Task FollowArtist(int artistId)
        {
            await ValidateThatArtistExists(artistId);
            await ValidateThatArtistIsNotFollowed(artistId);
            await AddArtistFollow(artistId);
        }

        public async Task FollowLabel(int labelId)
        {
            await ValidateThatLabelExists(artistId);
            await ValidateThatLabelIsNotFollowed(artistId);
            await AddLabelFollow(artistId);
        }

        public async Task RemoveFromWishlist(int recordId)
        {
            await ValidateThatRecordIsInWishlist(recordId);
            await RemoveRecordFromWishList(recordId);
        }

        public async Task UnfollowArtist(int artistId)
        {
            await ValidateThatArtistIsFollowed(artistId);
            await RemoveArtistFollow(artistId);
        }

        public Task UnfollowLabel(int labelId)
        {
            await ValidateThatLabelIsFollowed(artistId);
            await RemoveLabelFollow(artistId);
        }

        private async Task AddRecordToWishlist(int recordId)
        {
            await _wishes.Add(new Wish { RecordId = recordId, UserId = _currentUserId });
            await _wishes.SaveChanges();
        }

        private async Task ValidateThatRecordIsNotInWishlist(int recordId)
        {
            if (await _wishes.Exists(recordId, _currentUserId))
            {
                throw new ArgumentException("Record is already in wishlist!");
            }
        }

        private async Task ValidateThatRecordExists(int recordId)
        {
            if (!await _records.Exists(recordId))
            {
                throw new ArgumentException("Invalid record!");
            }
        }
    }
}
