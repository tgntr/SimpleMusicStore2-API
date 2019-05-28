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
    }
}
