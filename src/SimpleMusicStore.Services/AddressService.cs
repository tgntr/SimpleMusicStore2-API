using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addresses;
        public AddressService(IAddressRepository addresses)
        {
            _addresses = addresses;
        }
        public Task<bool> Exists(int id)
        {
            return _addresses.Exists(id);
        }

        public Task<bool> Exists(int id, string userId)
        {
            return _addresses.Exists(id, userId);
        }

        public Task<AddressDto> Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AddressDto>> FindAll(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOwner(int addressId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
