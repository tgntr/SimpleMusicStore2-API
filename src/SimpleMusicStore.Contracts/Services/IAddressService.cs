using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts.Services
{
    public interface IAddressService
    {
        Task<bool> Exists(int id);
        Task<bool> Exists(int id, string userId);
        Task<AddressDto> Find(int id);
        Task<IEnumerable<AddressDto>> FindAll(string userId);
        Task<bool> IsOwner(int addressId, string userId);
    }
}
