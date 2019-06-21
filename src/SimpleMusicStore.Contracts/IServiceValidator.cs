using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Validators
{
    public interface IServiceValidator
    {
        Task ItemIsInStock(int itemId, IDictionary<int, int> items);
        void ItemIsInCart(int itemId, IDictionary<int, int> items);
        Task RecordIsNotInStore(int id);
        Task LabelExists(int labelId);
        Task LabelIsNotFollowed(int labelId);
        Task ArtistIsNotFollowed(int artistId);
        Task ArtistExists(int artistId);
        Task RecordExists(int recordId);
        Task RecordIsNotInWishlist(int recordId);
        void CartIsNotEmpty(IDictionary<int, int> items);
        Task CredentialsAreValid(User user, string pasword);
        Task AddressIsValid(int id);
    }
}
