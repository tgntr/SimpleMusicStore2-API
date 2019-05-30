using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.AuthenticationProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IServiceValidations
    {
        Task ItemIsInStock(int itemId);
        void ItemIsInCart(int itemId);
        Task ItemExists(int itemId);
        Task RecordIsNotInStore(int id);
        Task LabelDoesNotExist(int discogsId);
        Task ArtistDoesNotExist(int discogsId);
        Task LabelIsFollowed(int labelId);
        Task ArtistIsFollowed(int artistId);
        Task RecordIsInWishlist(int recordId);
        Task LabelExists(int labelId);
        Task LabelIsNotFollowed(int labelId);
        Task ArtistIsNotFollowed(int artistId);
        Task ArtistExists(int artistId);
        Task RecordExists(int recordId);
        Task RecordIsNotInWishlist(int recordId);
        void CartIsNotEmpty();
        Task AddressIsValid(int id);
        Task CredentialsAreValid(User user, string password);
    }
}
