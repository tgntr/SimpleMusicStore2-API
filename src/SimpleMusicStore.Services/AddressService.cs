using AutoMapper;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Entities;
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
        private readonly IServiceValidations _validator;
        private readonly IMapper _mapper;
        private readonly IClaimAccessor _currentUser;

        public AddressService(IAddressRepository addresses, IServiceValidations validator, IMapper mapper, IClaimAccessor currentUser)
        {
            _addresses = addresses;
            _validator = validator;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task Add(AddressDetails newAddress)
        {
            var addressEntity = CreateAddress(newAddress);
            await _addresses.Add(addressEntity);
            await _addresses.SaveChanges();
        }

        public async Task Remove(int id)
        {
            await _validator.AddressIsValid(id);
            await _addresses.Remove(id);
            await _addresses.SaveChanges();
        }

        public async Task Edit(AddressDetails newAddress)
        {
            await _validator.AddressIsValid(newAddress.Id);
            await _addresses.Edit(newAddress);
            //TODO should i move all savechanges inside the repositories?
            await _addresses.SaveChanges();
        }
        private Address CreateAddress(AddressDetails address)
        {
            //TODO should i do all the mappings inside the repositories?
            var addressEntity = _mapper.Map<Address>(address);
            addressEntity.UserId = _currentUser.Id;
            return addressEntity;
        }

    }
}
