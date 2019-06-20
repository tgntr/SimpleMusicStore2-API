using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.ServiceValidations
{
    public class ServiceValidator : IServiceValidator
    {
        private readonly IAddressRepository _addresses;
        private readonly IWishRepository _wishes;
        private readonly IRecordRepository _records;
        private readonly IArtistFollowRepository _artistFollows;
        private readonly IArtistRepository _artists;
        private readonly ILabelRepository _labels;
        private readonly ILabelFollowRepository _labelFollows;
        private readonly IOrderRepository _orders;
        private readonly UserManager<User> _users;
        private readonly IClaimAccessor _currentUser;

        public ServiceValidator(
            IAddressRepository addresses,
            IClaimAccessor currentUser,
            IWishRepository wishes,
            IRecordRepository records,
            IArtistFollowRepository artistFollows,
            IArtistRepository artists,
            ILabelRepository labels,
            ILabelFollowRepository labelFollows,
            IOrderRepository orders,
            UserManager<User> users)
        {
            _addresses = addresses;
            _wishes = wishes;
            _records = records;
            _artistFollows = artistFollows;
            _artists = artists;
            _labels = labels;
            _labelFollows = labelFollows;
            _orders = orders;
            _users = users;
            _currentUser = currentUser;
        }
        public async Task AddressIsValid(int id)
        {
            if (!await _addresses.Exists(id, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.INVALID_ADDRESS);
        }

        public void CartIsNotEmpty(IDictionary<int, int> items)
        {
            if (items.Count == 0)
                throw new OperationCanceledException(ErrorMessages.EMPTY_CART);
        }

        public async Task RecordIsNotInWishlist(int recordId)
        {
            if (await _wishes.Exists(recordId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.RECORD_IS_IN_WISHLIST);
        }

        public async Task RecordExists(int recordId)
        {
            if (!await _records.Exists(recordId))
                throw new ArgumentException(ErrorMessages.INVALID_RECORD);
        }

        public async Task ArtistIsNotFollowed(int artistId)
        {
            if (await _artistFollows.Exists(artistId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.ARTIST_IS_FOLLOWED);
        }

        public async Task ArtistExists(int artistId)
        {
            if (!await _artists.Exists(artistId))
                throw new ArgumentException(ErrorMessages.INVALID_ARTIST);
        }

        public async Task LabelIsNotFollowed(int labelId)
        {
            if (await _labelFollows.Exists(labelId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.LABEL_IS_FOLLOWED);
        }

        public async Task LabelExists(int labelId)
        {
            if (!await _labels.Exists(labelId))
                throw new ArgumentException(ErrorMessages.INVALID_LABEL);
        }

        public async Task RecordIsInWishlist(int recordId)
        {
            if (!await _wishes.Exists(recordId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.RECORD_NOT_IN_WISHLIST);
        }

        public async Task ArtistIsFollowed(int artistId)
        {
            if (!await _artistFollows.Exists(artistId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.ARTIST_NOT_FOLLOWED);
        }

        public async Task LabelIsFollowed(int labelId)
        {
            if (!await _labelFollows.Exists(labelId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.LABEL_NOT_FOLLOWED);
        }

        public async Task RecordIsNotInStore(int id)
        {
            if (await _records.Exists(id))
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

            if (await _records.Availability(itemId) <= quantity)
                throw new ArgumentException(ErrorMessages.UNAVAILABLE_QUANTITY);
        }

        public async Task CredentialsAreValid(User user, string password)
        {
            if (!await _users.CheckPasswordAsync(user, password))
                throw new ArgumentException(ErrorMessages.INVALID_CREDENTIALS);
        }

        public async Task OrderIsValid(int orderId)
        {
            if (!await _orders.Exists(orderId, _currentUser.Id))
                throw new ArgumentException(ErrorMessages.INVALID_ORDER);
        }

        public void SearchTermIsNotEmpty(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                throw new ArgumentException(ErrorMessages.INVALID_SEARCH_TERM);
        }
    }
}
