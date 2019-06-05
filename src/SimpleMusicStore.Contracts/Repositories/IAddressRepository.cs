using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<bool> Exists(int id);
        Task<bool> Exists(int id, string userId);
        //todo ICollection vs IEnumerable
        IEnumerable<AddressDetails> FindAll(string userId);
    }
}
