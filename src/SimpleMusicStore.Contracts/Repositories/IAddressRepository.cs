using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        IEnumerable<AddressDetails> FindAll(string userId);
        Task Edit(AddressDetails address);
        Task Remove(int addressId);
        Task<bool> Exists(int id, string userId);
    }
}
