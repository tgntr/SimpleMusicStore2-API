using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Data;
using SimpleMusicStore.Entities;
using SimpleMusicStore.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Repositories
{
    public class AddressRepository : DbRepository<Address>, IAddressRepository
    {
        private readonly IMapper _mapper;

        public AddressRepository(SimpleMusicStoreDbContext db, IMapper mapper)
            : base(db)
        {
            _mapper = mapper;
        }

        public async Task Edit(AddressDetails address)
        {
            var addressEntity = await Find(address.Id);
            addressEntity.Street = address.Street;
            addressEntity.City = address.City;
            addressEntity.Country = address.Country;
        }

        public Task<bool> Exists(int id, string userId)
        {
            return _set.AnyAsync(a => a.Id == id && a.UserId == userId);
        }

        public IEnumerable<AddressDetails> FindAll(string userId)
        {
            //TODO is it an okay way to map stuff? Is it good to map things here?
            return _set.Where(a => a.UserId == userId).Select(_mapper.Map<AddressDetails>);
        }

        public async Task Remove(int addressId)
        {
            var address = await Find(addressId);
            address.IsActive = false;
        }

        private Task<Address> Find(int id)
        {
            return _set.FirstAsync(a => a.Id == id);
        }
    }
}
