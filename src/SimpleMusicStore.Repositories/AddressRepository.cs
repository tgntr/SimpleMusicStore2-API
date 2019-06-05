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

        public Task<bool> Exists(int id)
        {
            return _set.AnyAsync(a => a.Id == id);
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
    }
}
