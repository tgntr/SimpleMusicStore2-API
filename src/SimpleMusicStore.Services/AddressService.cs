using SimpleMusicStore.Contracts.Auth;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Contracts.Validators;
using SimpleMusicStore.Models.Binding;
using SimpleMusicStore.Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _db;
        private readonly IServiceValidator _validator;
        private readonly IClaimAccessor _currentUser;

        public AddressService(IUnitOfWork db, IServiceValidator validator, IClaimAccessor currentUser)
        {
            _db = db;
            _validator = validator;
            _currentUser = currentUser;
        }

        public async Task Add(NewAddress address)
        {
            address.UserId = _currentUser.Id;
            await _db.Addresses.Add(address);
            await _db.SaveChanges();
        }

        public async Task Remove(int id)
        {
            //todo check if it's user's address
            await _db.Addresses.Remove(id);
            await _db.SaveChanges();
        }

        public async Task Edit(AddressEdit address)
        {
            //todo check if it's user's address
            await _db.Addresses.Edit(address);
            await _db.SaveChanges();
        }

        public IEnumerable<AddressDetails> FindAll()
        {
            //_validator.AccessibleByCurrentUser(userId);
            return _db.Addresses.FindAll(_currentUser.Id);
        }
    }
}
