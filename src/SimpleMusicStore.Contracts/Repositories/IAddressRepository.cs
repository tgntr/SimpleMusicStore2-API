using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IAddressRepository
    {
        IEnumerable<AddressDetails> FindAll(string userId);
        Task Add(NewAddress address);
        Task Edit(AddressEdit address);
        Task Remove(int addressId);
        Task<bool> Exists(int id, string userId);
    }
}
