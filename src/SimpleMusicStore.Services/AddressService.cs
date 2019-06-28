using AutoMapper;
using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _db;

        public AddressService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task Add(NewAddress address)
        {
            address.UserId = _db.CurrentUser.Id;
            await _db.Addresses.Add(address);
            await _db.SaveChanges();
        }

        public async Task Remove(int id)
        {
            await _db.Addresses.Remove(id);
            await _db.SaveChanges();
        }

        public async Task Edit(AddressEdit address)
        {
            await _db.Addresses.Edit(address);
            await _db.SaveChanges();
        }

        public IEnumerable<AddressDetails> FindAll(string userId)
        {
            _db.Validator.AccessibleByCurrentUser(userId);
            return _db.Addresses.FindAll(userId);
        }
    }
}
