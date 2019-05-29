using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class ServiceValidations : IServiceValidations
    {
        private readonly IAddressRepository _addresses;
        private readonly ShoppingCart _cart;
        private readonly IWishRepository _wishes;
        private readonly IRecordRepository _records;
        private readonly IArtistFollowRepository _artistFollows;
        private readonly IArtistRepository _artists;
        private readonly ILabelRepository _labels;
        private readonly ILabelFollowRepository _labelFollows;
        private readonly UserManager<User> _users;
        private readonly IClaimAccessor _currentUser;

        public ServiceValidations(
            IAddressRepository addresses,
            IClaimAccessor currentUser,
            ShoppingCart cart,
            IWishRepository wishes,
            IRecordRepository records,
            IArtistFollowRepository artistFollows,
            IArtistRepository artists,
            ILabelRepository labels,
            ILabelFollowRepository labelFollows,
            UserManager<User> users)
        {
            _addresses = addresses;
            _cart = cart;
            _wishes = wishes;
            _records = records;
            _artistFollows = artistFollows;
            _artists = artists;
            _labels = labels;
            _labelFollows = labelFollows;
            _users = users;
            _currentUser = currentUser;
        }
        public async Task AddressIsValid(int id)
        {
            if (!await _addresses.Exists(id, _currentUser.Id))
                throw new ArgumentException("Invalid address!");
        }

        public void CartIsNotEmpty()
        {
            if (_cart.IsEmpty())
                throw new OperationCanceledException("Cart is empty!");
        }

        public async Task RecordIsNotInWishlist(int recordId)
        {
            if (await _wishes.Exists(recordId, _currentUser.Id))
                throw new ArgumentException("Record is already in wishlist!");
        }

        public async Task RecordExists(int recordId)
        {
            if (!await _records.Exists(recordId))
                throw new ArgumentException("Record does not exist!");
        }

        public async Task ArtistIsNotFollowed(int artistId)
        {
            if (await _artistFollows.Exists(artistId, _currentUser.Id))
                throw new ArgumentException("Artist is already followed!");
        }

        public async Task ArtistExists(int artistId)
        {
            if (!await _artists.Exists(artistId))
                throw new ArgumentException("Artist does not exist!");
        }

        public async Task LabelIsNotFollowed(int labelId)
        {
            if (await _labelFollows.Exists(labelId, _currentUser.Id))
                throw new ArgumentException("Label is already followed!");
        }

        public async Task LabelExists(int labelId)
        {
            if (!await _labels.Exists(labelId))
                throw new ArgumentException("Label does not exist!");
        }

        public async Task RecordIsInWishlist(int recordId)
        {
            if (!await _wishes.Exists(recordId, _currentUser.Id))
                throw new ArgumentException("Record is not in wishlist!");
        }

        public async Task ArtistIsFollowed(int artistId)
        {
            if (!await _artistFollows.Exists(artistId, _currentUser.Id))
                throw new ArgumentException("Artist is not followed!");
        }

        public async Task LabelIsFollowed(int labelId)
        {
            if (!await _labelFollows.Exists(labelId, _currentUser.Id))
                throw new ArgumentException("Label is not followed!");
        }

        public async Task ArtistDoesNotExist(int discogsId)
        {
            if (await _artists.Exists(discogsId))
                return;
        }

        public async Task LabelDoesNotExist(int discogsId)
        {
            if (await _labels.Exists(discogsId))
                return;
        }

        public async Task RecordIsNotInStore(int id)
        {
            if (await _records.Exists(id))
                throw new ArgumentException("Record is already in store!");
        }

        public async Task ItemExists(int itemId)
        {
            if (!await _records.Exists(itemId))
                throw new ArgumentException("Invalid record id!");
        }

        public void ItemIsInCart(int itemId)
        {
            if (!_cart.Items.ContainsKey(itemId))
                throw new ArgumentException("Cart does not contain such record!");
        }

        public async Task ItemIsInStock(int itemId)
        {
            var quantity = 0;
            if (_cart.Items.ContainsKey(itemId))
                quantity = _cart.Items[itemId];

            if (await _records.Availability(itemId) <= quantity)
                throw new ArgumentException("Required quantity is not available!");
        }

        public async Task CredentialsAreValid(User user, string password)
        {
            if (!await _users.CheckPasswordAsync(user, password))
                throw new ArgumentException("Invalid credentials");
        }
    }
}
