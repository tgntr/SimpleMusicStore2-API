using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.ServiceValidations
{
    public class ServiceValidator : IServiceValidator
    {
        //todo unit of work
        private readonly IClaimAccessor _currentUser;
        private readonly IUnitOfWork _db;

        public ServiceValidator(
            IUnitOfWork db,
            IClaimAccessor currentUser)
        {
            _currentUser = currentUser;
            _db = db;
        }

        public void CartIsNotEmpty(IDictionary<int, int> items)
        {
            if (!items.Any())
                throw new OperationCanceledException(ErrorMessages.EMPTY_CART);
        }

        public async Task RecordIsNotInWishlist(int recordId)
        {
            if (await _db.Wishes.Exists(recordId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.RECORD_ALREADY_IN_WISHLIST);
        }

        public async Task RecordExists(int recordId)
        {
            if (!await _db.Records.Exists(recordId))
                throw new ArgumentException(ErrorMessages.INVALID_RECORD);
        }

        public async Task ArtistIsNotFollowed(int artistId)
        {
            if (await _db.ArtistFollows.Exists(artistId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.ARTIST_ALREADY_FOLLOWED);
        }

        public async Task ArtistExists(int artistId)
        {
            if (!await _db.Artists.Exists(artistId))
                throw new ArgumentException(ErrorMessages.INVALID_ARTIST);
        }

        public async Task LabelIsNotFollowed(int labelId)
        {
            if (await _db.LabelFollows.Exists(labelId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.LABEL_ALREADY_FOLLOWED);
        }

        public async Task LabelExists(int labelId)
        {
            if (!await _db.Labels.Exists(labelId))
                throw new ArgumentException(ErrorMessages.INVALID_LABEL);
        }

        public async Task RecordIsNotInStore(int id)
        {
            if (await _db.Records.Exists(id))
                throw new ArgumentException(ErrorMessages.RECORD_ALREADY_EXISTS);
        }

        public void ItemIsInCart(int itemId, IDictionary<int, int> items)
        {
            if (!items.ContainsKey(itemId))
                throw new ArgumentException(ErrorMessages.ITEM_NOT_IN_CART);
        }

        public async Task ItemIsInStock(int itemId, IDictionary<int, int> items)
        {
            var quantity = 0;
            if (items.ContainsKey(itemId))
                quantity = items[itemId];

            if (await _db.Records.Availability(itemId) <= quantity)
                throw new ArgumentException(ErrorMessages.UNAVAILABLE_QUANTITY);
        }

        public async Task ItemsAreInStock(IDictionary<int, int> items)
        {
            foreach (var item in items)
            {
                if (await _db.Records.Availability(item.Key) < item.Value)
                    throw new ArgumentException(ErrorMessages.UNAVAILABLE_QUANTITY);
            }
        }

        public async Task AddressIsValid(int id)
        {
            if (!await _db.Addresses.Exists(id, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.INVALID_ADDRESS);
        }

        public void AccessibleByCurrentUser(int userId)
        {
            //TODO make them accessible by admins when u implement roles
            if (userId != _currentUser.Id)
                throw new ArgumentException(ErrorMessages.INACCESSIBLE);
        }

        public async Task IsCommentAuthor(int commentId)
        {
			if (!await _db.Comments.IsAuthor(commentId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.FORBIDDEN_COMMENT_DELETION);
        }
    }
}
